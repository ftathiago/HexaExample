using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace HexaEmployee.EfInfraData.Contexts
{
    [ExcludeFromCodeCoverage]
    public class DataContextFactory : IDesignTimeDbContextFactory<HexaEmployeeDbContext>
    {
        public HexaEmployeeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HexaEmployeeDbContext>();
            var configuration = SetupSource();
            Console.WriteLine(configuration.GetConnectionString("Default"));
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));
            return new HexaEmployeeDbContext(optionsBuilder.Options);
        }

        private static IConfiguration SetupSource()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();
        }
    }
}
