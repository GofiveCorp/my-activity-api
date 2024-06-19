using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Activity.Configuration {
    public class ApiKeyFilter : Attribute, IAsyncActionFilter {
        private const string ApiKeyHeaderName = "X-API-KEY";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey)) {
                context.Result = new ContentResult() {
                    StatusCode = 401,
                    Content = "API Key was not provided"
                };
                return;
            }

            await next();
        }
    }
}
