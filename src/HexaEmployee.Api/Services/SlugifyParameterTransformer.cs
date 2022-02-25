using HexaEmployee.Shared.Extensions;
using Microsoft.AspNetCore.Routing;

namespace HexaEmployee.Api.Services
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value) => value?
            .ToString()
            .ToSlugify();
    }
}
