using HexaEmployee.Domain.Repositories;
using HexaEmployee.EfInfraData.Contexts;
using HexaEmployee.EfInfraData.Repositories;
using HexaEmployee.Shared.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace HexaEmployee.EfInfraData.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class InfraDataServicesExtension
    {
        public static IServiceCollection AddEfInfraData(this IServiceCollection services) =>
            services
                .AddScoped<ISampleRepository, SampleRepository>()
                .AddDbContext<HexaEmployeeDbContext>((provider, options) =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    options.UseNpgsql(configuration.GetConnectionString("Default"));
                })
                .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
