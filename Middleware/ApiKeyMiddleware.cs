namespace PortfolioAPI.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private readonly ILogger<ApiKeyMiddleware> _logger;
        private const string APIKEY = "x-api-key";

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration config, ILogger<ApiKeyMiddleware> logger)
        {
            _next = next;
            _config = config;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try {

                if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
                {
                    // Key was not present in the request headers
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new {success = false, error = "API key was not provided."});
                    return;
                }

                string apiKey = _config.GetValue<string>(APIKEY);
                if (!apiKey.Equals(extractedApiKey))
                {
                    // Key provided was incorrect
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new {success = false, error = "The provided API key is incorrect."});
                    return;
                }
            }
            
            catch (Exception ex)
            {
                _logger.LogError(0, ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new {success = false, error = "An internal server error has occurred."});
                return;
            }

            await _next(context);
        }
    }
}