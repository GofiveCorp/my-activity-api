using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Activity.Configuration {
    public class AddRequiredHeaderParameter : IOperationFilter {
        public void Apply(OpenApiOperation operation, OperationFilterContext context) {
            operation.Parameters ??= [];

            operation.Parameters.Add(new OpenApiParameter() {
                Name = "X-API-KEY",
                In = ParameterLocation.Header,
                Required = true
            });
        }
    }
}
