using Microsoft.EntityFrameworkCore;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Domain.Entities;
using System;

namespace SampleUserManagement.Infrastructure.Context
{
    public class PostgresDbContext : DbContext, IContext
    {
        public PostgresDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
