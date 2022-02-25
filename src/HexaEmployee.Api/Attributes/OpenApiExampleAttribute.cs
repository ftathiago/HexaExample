using System;
using System.Diagnostics.CodeAnalysis;

namespace HexaEmployee.Api.Attributes
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Property)]
    public class OpenApiExampleAttribute : Attribute
    {
        public OpenApiExampleAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}
