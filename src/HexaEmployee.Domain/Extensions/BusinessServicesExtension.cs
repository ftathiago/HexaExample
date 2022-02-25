using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace HexaEmployee.Domain.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class BusinessServicesExtension
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services) =>
            services;
    }
}
