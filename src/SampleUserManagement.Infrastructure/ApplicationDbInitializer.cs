using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleUserManagement.Application.Common.Interfaces;
using SampleUserManagement.Infrastructure.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Infrastructure
{
    public class ApplicationDbInitializer : IHostedService
    {
        private readonly ILogger<ApplicationDbInitializer> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ApplicationDbInitializer(ILogger<ApplicationDbInitializer> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            IContext context;

            string? dbProvider = configuration.GetSection("Infrastructure:Database:Provider").Value;
            switch (dbProvider)
            {
                case "Postgres":
                    context = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();
                    break;
                default:
                    throw new ArgumentException($"Selected database provider {dbProvider} is not supported");
            }

            _logger.LogInformation("Running migrations for {Context}", nameof(PostgresDbContext));
            await context.Database.MigrateAsync(cancellationToken: cancellationToken);
            _logger.LogInformation("Migrations applied successfully");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
