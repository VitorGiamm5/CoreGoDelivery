using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace CoreGoDelivery.Api.Swagger;

public sealed class DefaultValuesOperation : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        foreach (var parameter in operation.Parameters)
        {
            var property = context.MethodInfo?.DeclaringType?.GetProperty(parameter.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property != null)
            {
                var defaultvalueAttribute = property?.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultvalueAttribute != null)
                {
                    parameter.Schema.Default = new OpenApiString(defaultvalueAttribute?.Value?.ToString());
                }
            }
        }
    }
}
