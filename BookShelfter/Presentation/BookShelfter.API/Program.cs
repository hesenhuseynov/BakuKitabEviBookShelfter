using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using BookShelfter.API.Middlewares;
using BookShelfter.Application;
using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.Features.Commands.Basket.AddItem;
using BookShelfter.Infrastructure;
using BookShelfter.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.Development.json")
//    .Build();
var environment = builder.Environment;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();



Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    //.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();



builder.Host.UseSerilog();

BasketHelper.ConfigureLogger(Log.Logger);

builder.Logging.SetMinimumLevel(LogLevel.Information);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin", builder =>
//        builder.WithOrigins("http://localhost:4200", "https://872e-188-253-208-172.ngrok-free.app")
//            .AllowAnyHeader()
//    );
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.WithOrigins("https://bookshelfterclient-15631.web.app",
                "https://www.bakukitabevi.com",
                configuration["Application:AngularBaseUrl"])
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});


//options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };




    //options.Events = new JwtBearerEvents
    //{
    //    OnAuthenticationFailed = context =>
    //    {
    //        Log.Error("Authentication failed: {Exception}", context.Exception);
    //        return Task.CompletedTask;
    //    },

    //    OnTokenValidated = context =>
    //    {
    //        Log.Information("Token validated for user :{User}", context.Principal.Identity.Name);
    //        return Task.CompletedTask;
    //    }

    //};


}).AddGoogle(options =>
{
    options.ClientId = configuration["Google:ClientId"];
    options.ClientSecret = configuration["Google:ClientSecret"];
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
    //options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
    //    .RequireAuthenticatedUser().Build();
});

builder.Services.AddMemoryCache();
builder.Services.AddInfrasturctureServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationService();

builder.Services.AddMediatR(typeof(ICacheService).Assembly);  

builder.Services.AddMiniProfiler(options =>
{
    options.RouteBasePath = "/profiler";
    // options.Storage = new MemoryCacheStorage(TimeSpan.FromMinutes(60));
    options.ResultsAuthorize = request => true;
    options.ResultsListAuthorize = request => true;
}).AddEntityFramework();

 


builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddHealthChecks();


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("Basic", _options =>
    {
        _options.Window = TimeSpan.FromSeconds(12);
        _options.PermitLimit = 21;
        _options.QueueLimit = 2;
        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();




//app.UseMiddleware<ServerTimeLoggingMiddleware>();

//if (app.Environment.IsDevelopment())
//{
//    //app.UseSwagger();
//    //app.UseSwaggerUI();


//}


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowSpecificOrigin");
app.UseSerilogRequestLogging();





app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();


app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");

});


app.MapControllers();



app.Run();
