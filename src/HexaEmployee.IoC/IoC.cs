using HexaEmployee.Domain.Extensions;
using HexaEmployee.Domain.Notifications;
using HexaEmployee.EfInfraData.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace HexaEmployee.IoC
{
    [ExcludeFromCodeCoverage]
    public static class IoC
    {
        public static IServiceCollection ProjectsIocConfig(this IServiceCollection services) =>
            services
                .AddBusiness()
                .AddEfInfraData()
                .AddScoped<INotification, Notification>();
    }
}
