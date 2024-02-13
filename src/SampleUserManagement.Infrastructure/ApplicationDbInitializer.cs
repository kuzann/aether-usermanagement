using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleUserManagement.Application.Common.Interfaces;
using SampleUserManagement.Domain.Entities;
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

            _logger.LogInformation($"Start running migrations for {nameof(PostgresDbContext)}");
            await context.Database.MigrateAsync(cancellationToken: cancellationToken);
            _logger.LogInformation("Migrations applied successfully.");

            bool performDataSeed = configuration.GetValue<bool>("Infrastructure:Database:PerformDataSeed");
            _logger.LogInformation($"Start performing data seed. PerformDataSeed config: {performDataSeed}");
            if (performDataSeed)
            {
                var userDb = context.Set<User>();
                if (userDb != null && userDb.FirstOrDefault() == null) 
                {
                    var faker = new Faker<User>()
                        .RuleFor(u => u.Email, f => f.Person.Email)
                        .RuleFor(u => u.Password, f => f.Random.AlphaNumeric(10))
                        .RuleFor(u => u.FullName, f => f.Person.FullName)
                        .RuleFor(u => u.DateOfBirth, f => f.Date.BetweenDateOnly(new DateOnly(1950, 1, 1), new DateOnly(2005, 12, 31)))
                        .RuleFor(u => u.CreatedBy, f => f.PickRandom(new string[] { "SYSTEM" }))
                        .RuleFor(u => u.CreatedAt, f => f.Date.PastOffset(10));

                    var users = faker.Generate(200);
                    userDb.AddRange(users);
                    await context.SaveChangesAsync();
                }
            }
            _logger.LogInformation("Data seeding process finished.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
