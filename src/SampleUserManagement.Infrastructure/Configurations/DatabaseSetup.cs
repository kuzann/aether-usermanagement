using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Infrastructure.Context;
using SampleUserManagement.Infrastructure.Repositories;

namespace SampleUserManagement.Infrastructure.Configurations
{
    public static class DatabaseSetup
    {
        public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            string? dbProvider = configuration.GetSection("Infrastructure:Database:Provider").Value;
            switch (dbProvider)
            {
                case "Postgres":
                    services.AddDbContextPool<PostgresDbContext>(o =>
                    {
                        o.UseNpgsql(configuration.GetConnectionString("Default"));
                        o.UseExceptionProcessor();
                    });
                    services.AddScoped<IContext, PostgresDbContext>();
                    break;
                default:
                    throw new ArgumentException($"Selected database provider {dbProvider} is not supported");
            }
            services.AddHostedService<ApplicationDbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
