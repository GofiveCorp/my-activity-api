using Activity.Data;
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

            var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
            var apiKeySetting = await dbContext.ConfigurationSettings.FindAsync("ApiKey");
            var apiKey = apiKeySetting?.Value;

            if (apiKey == null || !apiKey.Equals(extractedApiKey)) {
                context.Result = new ContentResult() {
                    StatusCode = 403,
                    Content = "Unauthorized Access"
                };
                return;
            }

            await next();
        }
    }
}
