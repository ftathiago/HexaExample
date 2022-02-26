using HexaEmployee.Api.Configurations;
using HexaEmployee.Api.Models.OpenApiSecurity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace HexaEmployee.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerExtension
    {
        public static IServiceCollection ConfigSwagger(this IServiceCollection services) => services
            .AddSwaggerGen(options =>
            {
                OpenApiSecurityScheme securityScheme = new OpenApiBearerSecurityScheme();
                OpenApiSecurityRequirement securityRequirement = new OpenApiBearerSecurityRequirement(securityScheme);

                options.AddSecurityDefinition("Bearer", securityScheme);

                options.AddSecurityRequirement(securityRequirement);

                options.ExampleFilters();
                options.OperationFilter<AddResponseHeadersFilter>();
                options.CustomSchemaIds(type => type.FullName);
                options.LoadDocumentationFiles();
            })
            .AddSwaggerExamplesFromAssemblyOf<Startup>()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        private static void LoadDocumentationFiles(this SwaggerGenOptions options)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var xmlDocumentationFile = $"{assembly.GetName().Name}.xml";
                var xmlDocumentationPath = Path.Combine(AppContext.BaseDirectory, xmlDocumentationFile);
                if (File.Exists(xmlDocumentationPath))
                {
                    options.IncludeXmlComments(xmlDocumentationPath);
                }
            }
        }
    }
}
