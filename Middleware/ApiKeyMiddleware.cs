using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace PortfolioAPI.Middleware
{
    public class ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ApiKeyMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try {

                if (!context.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
                {
                    // Key was not present in the request headers
                    context.Response.StatusCode = 401;
                    var problemDetails = GetProblemDetails("API key was not provided.", 401, context);
                    await context.Response.WriteAsJsonAsync(problemDetails);
                    return;
                }

                string apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? string.Empty;
                if ((!string.IsNullOrEmpty(apiKey)) && (!apiKey.Equals(extractedApiKey)))
                {
                    // Key provided was incorrect
                    context.Response.StatusCode = 401;
                    var problemDetails = GetProblemDetails("The provided API key is incorrect.", 401, context);
                    await context.Response.WriteAsJsonAsync(problemDetails);
                    return;
                }
            }
            
            catch (Exception ex)
            {
                _logger.LogError(0, ex, ex.Message);
                context.Response.StatusCode = 500;
                var problemDetails = GetProblemDetails("An internal server error has occurred.", 500, context);
                await context.Response.WriteAsJsonAsync(problemDetails);
                return;
            }

            await _next(context);
        }

        private static ProblemDetails GetProblemDetails(string errorDescription, int statusCode, HttpContext httpContext)
        {
            return new ProblemDetails
            {
                Title = ReasonPhrases.GetReasonPhrase(statusCode),
                Detail = errorDescription,
                Instance = httpContext.Request.Path
            };
        }
    }
}