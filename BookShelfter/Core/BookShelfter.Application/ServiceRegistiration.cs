    using BookShelfter.Application.Behaviors;
    using BookShelfter.Application.Validators;
    using BookShelfter.Application.Validators.Books;
    using BookShelfter.Application.ViewModels.Books;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using Microsoft.Extensions.DependencyInjection;
    using MediatR;
    using Serilog;

    namespace BookShelfter.Application;

    public static class ServiceRegistiration
    {

        public static void AddApplicationService(this IServiceCollection collection)
        {


            collection.AddMediatR(typeof(ServiceRegistiration).Assembly);
            collection.AddValidatorsFromAssembly(typeof(ServiceRegistiration).Assembly);
            collection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            collection.AddHttpClient();
            collection.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            collection.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            collection.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheInValidationBehavior<,>));


            collection.AddSingleton(Log.Logger);


        }
    }