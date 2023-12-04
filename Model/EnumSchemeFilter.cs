using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Producor_Web_API.Model
{
    public class EnumSchemeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum = Enum.GetNames(context.Type)
                              .Select(name => new OpenApiString(name))
                              .ToList<IOpenApiAny>();
            }
        }
    }
}
