using Microsoft.AspNetCore.WebUtilities;

namespace PortfolioAPI.Middleware
{
    public class ApiKeyMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
            {
                // Key was not present in the request headers
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var problemDetails = GetProblemDetails("API key was not provided.", StatusCodes.Status401Unauthorized, context);
                await problemDetails.ExecuteAsync(context);
                return;
            }

            string apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? string.Empty;
            if ((!string.IsNullOrEmpty(apiKey)) && (!apiKey.Equals(extractedApiKey)))
            {
                // Key provided was incorrect
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var problemDetails = GetProblemDetails("The provided API key is incorrect.", StatusCodes.Status401Unauthorized, context);
                await problemDetails.ExecuteAsync(context);
                return;
            }
            
            await _next(context);
        }

        private static IResult GetProblemDetails(string errorDescription, int statusCode, HttpContext httpContext)
        {
            return Results.Problem(
                title: ReasonPhrases.GetReasonPhrase(statusCode),
                statusCode: statusCode,
                detail: errorDescription,
                instance: httpContext.Request.Path
            );
        }
    }
}