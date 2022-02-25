using HexaEmployee.Api.Attributes;
using HexaEmployee.Api.Extensions;
using HexaEmployee.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace HexaEmployee.Api.Filters
{
    [ExcludeFromCodeCoverage]
    public class QueryFieldsOpenApiFilter : IOperationFilter
    {
        private const string ParameterNameFormat = "{0}[{1}]";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            ConfigureMultiParamDescriptors(
                context.ApiDescription
                    .ParameterDescriptions
                    .Where(descriptor => descriptor.Type.IsSubclassOf(typeof(QueryFields))),
                operation);
        }

        private static void ConfigureMultiParamDescriptors(
            IEnumerable<ApiParameterDescription> descriptors,
            OpenApiOperation operation)
        {
            foreach (var descriptor in descriptors)
            {
                var originalParameters = operation.Parameters
                    .Where(param => param.Name == descriptor.Name)
                    .ToList();

                originalParameters.ForEach(param =>
                {
                    operation.Parameters.Remove(param);
                    GenerateParametersFromQueryFieldSubclassProperties(param, descriptor.Type)
                        .ToList()
                        .ForEach(operation.Parameters.Add);
                });
            }
        }

        private static IEnumerable<OpenApiParameter> GenerateParametersFromQueryFieldSubclassProperties(
            OpenApiParameter originalDefinition,
            Type type) => type
            .GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.DeclaredOnly)
            .Select(property => new OpenApiParameter
            {
                Name = ParameterNameFormat
                    .Format(originalDefinition.Name.ToCamelCase(), property.Name.ToCamelCase()),
                Example = new OpenApiString(property.GetCustomAttribute<OpenApiExampleAttribute>()?.Value),
                Required = property.HasAttribute<RequiredAttribute>(),
                Description = property.GetCustomAttribute<DescriptionAttribute>()?.Description,
                Explode = true,
                In = originalDefinition.In,
                Schema = new()
                {
                    Format = "string",
                    Type = "string",
                },
            });
    }
}
