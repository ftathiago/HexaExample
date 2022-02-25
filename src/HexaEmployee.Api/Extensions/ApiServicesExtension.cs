using HexaEmployee.Api.Filters;
using HexaEmployee.Api.Services;
using HexaEmployee.IoC;
using HexaEmployee.WarmUp.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace HexaEmployee.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ApiServicesExtension
    {
        public static IServiceCollection AddApiIoc(this IServiceCollection services) =>
            services
                .AddDataDog()
                .SlugifyRouter()
                .ConfigSwagger()
                .ConfigureAuth()
                .AddEndpoints()
                .ConfigureApiVersioning()
                .AddExternalDependencies();

        private static IServiceCollection AddEndpoints(this IServiceCollection services) =>
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<ControllerExceptionFilter>();
                    options.Filters.Add<MessageFilter>();

                    options.Conventions.Add(new RouteTokenTransformerConvention(
                        new SlugifyParameterTransformer()));
                    options.Filters.Add<ControllerExceptionFilter>();
                    options.Filters.Add<MessageFilter>();
                })
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.DefaultIgnoreCondition =
                        JsonIgnoreCondition.WhenWritingNull).Services
                .AddHealthChecks().Services;

        private static IServiceCollection AddExternalDependencies(this IServiceCollection services) =>
            services
                .AddWarmUp(
                    logInfo => Log.Information(logInfo),
                    logError => Log.Error(logError),
                    logTrace => Log.Verbose(logTrace))
                .ProjectsIocConfig();

        private static IServiceCollection SlugifyRouter(this IServiceCollection services) =>
            services
                .Configure<RouteOptions>(options =>
                {
                    options.LowercaseQueryStrings = true;
                    options.LowercaseUrls = true;
                    options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
                });
    }
}
