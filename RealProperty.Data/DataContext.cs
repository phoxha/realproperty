using System;
using RealProperty.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RealProperty.Data
{
    public class DataContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var connection = environment == "Development" ? _configuration.GetConnectionString("DataContext") : Environment.GetEnvironmentVariable("ARCHYSOFT_DEMO_DATACONTEXT") ?? "";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
