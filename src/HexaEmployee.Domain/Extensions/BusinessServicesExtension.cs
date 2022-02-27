using HexaEmployee.Domain.Services;
using HexaEmployee.Domain.Services.Impl;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace HexaEmployee.Domain.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class BusinessServicesExtension
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services) =>
            services
                .AddScoped<IEmployeeService, EmployeeService>();
    }
}
