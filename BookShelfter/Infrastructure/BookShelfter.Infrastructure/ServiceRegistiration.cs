using BookShelfter.Application.Abstractions.Storage;
using BookShelfter.Application.Abstractions.Storage.Azure;
using BookShelfter.Infrastructure.Services.Storage;
using BookShelfter.Infrastructure.Services.Storage.Google;
using BookShelfter.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.Abstractions.Token;
using BookShelfter.Infrastructure.Services.Cache;
using BookShelfter.Infrastructure.Services.Email;
using BookShelfter.Infrastructure.Services.Token;

namespace BookShelfter.Infrastructure
{
    public static  class ServiceRegistiration
    {
        public static void AddInfrasturctureServices( this IServiceCollection serviceCollection,IConfiguration configuration)
        {
            serviceCollection.AddScoped<IStorageService,StorageService>();
            serviceCollection.AddScoped<IStorage, LocalStorage>();
            serviceCollection.AddScoped<ICacheService, MemoryCacheService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddTransient<IEmailService, EmailService>();

            serviceCollection.AddSingleton<IFileStorageService, GoogleCloudStorage>(provider =>
            {
                return new GoogleCloudStorage(configuration);
            });           


        }
        //public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        //{
        //    serviceCollection.AddScoped<IStorage, T>();
        //}

    }
}
