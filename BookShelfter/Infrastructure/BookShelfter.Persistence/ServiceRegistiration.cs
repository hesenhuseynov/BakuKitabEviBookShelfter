using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.Repositories.Basket;
using BookShelfter.Application.Repositories.Book;
using BookShelfter.Application.Repositories.Category;
using BookShelfter.Application.Repositories.Customer;
using BookShelfter.Application.Repositories.Order;
using BookShelfter.Application.Repositories.ProductImageFile;
using BookShelfter.Application.Repositories.Review;
using BookShelfter.Application.Repositories.User;
using BookShelfter.Domain.Entities.Identity;
using BookShelfter.Persistence.Book;
using BookShelfter.Persistence.Category;
using BookShelfter.Persistence.Contexts;
using BookShelfter.Persistence.Repositories.Basket;
using BookShelfter.Persistence.Repositories.BookImageFile;
using BookShelfter.Persistence.Repositories.Customer;
using BookShelfter.Persistence.Repositories.Order;
using BookShelfter.Persistence.Repositories.Review;
using BookShelfter.Persistence.Repositories.User;
using BookShelfter.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShelfter.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<BookShelfterDbContext>(options =>
        //    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<BookShelfterDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        })

            .AddEntityFrameworkStores<BookShelfterDbContext>()
            .AddDefaultTokenProviders();


        //services.ConfigureApplicationCookie(options =>
        //{
        //    options.Cookie.HttpOnly = true;
        //    //options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        //    options.LoginPath = "/Account/Login";
        //    options.AccessDeniedPath = "/Account/AccessDenied";
        //    options.SlidingExpiration = true;
        //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        //    options.Cookie.SameSite = SameSiteMode.Strict;
        //    options.Cookie.IsEssential = true;
        //});



        services.AddScoped<IBookReadRepository, BookReadRepository>();
        services.AddScoped<IBookWriteRepository, BookWriteRepository>();
        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
        services.AddScoped<IProductImageFileReadRepository, BookImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, BookImageFIleWriteRepository>();
        services.AddScoped<IBasketReadRepository, BasketReadRepository>();
        services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();


        services.AddScoped<IReviewReadRepository, ReviewReadRepository>();
        services.AddScoped<IReviewWriteRepository, ReviewWriteRepository>();
        services.AddScoped<IRoleService, RoleService>();


    }
}
