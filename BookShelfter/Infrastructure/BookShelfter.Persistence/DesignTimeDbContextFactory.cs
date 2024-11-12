using BookShelfter.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace BookShelfter.Persistence;

public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<BookShelfterDbContext>
{
    public BookShelfterDbContext CreateDbContext(string[] args)
    {

        //DbContextOptionsBuilder<BookShelfterDbContext> dbContextOptionsBuilder = new();
        //dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
        //return new(dbContextOptionsBuilder.Options);

         
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/BookShelfter.API"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .Build();

        
        var connectionString = configuration.GetConnectionString("DefaultConnection");


        var optionsBuilder = new DbContextOptionsBuilder<BookShelfterDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new BookShelfterDbContext(optionsBuilder.Options);








    }
}