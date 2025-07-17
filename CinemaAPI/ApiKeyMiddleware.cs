namespace CinemaAPI
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        public const string APIKEY_HEADER = "X-Api-Key";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration configuration) { 
        
        
        if(!context.Request.Headers.TryGetValue(APIKEY_HEADER, out var extractedApiKey)) {

                context.Response.StatusCode = 401;// Unathorized
                await context.Response.WriteAsync("API Key is missing");
                return;
        }
        
        var apiKey = configuration.GetValue<string>("ApiKey");
            if (!apiKey.Equals(extractedApiKey)) {

                context.Response.StatusCode = 401;// Unathorized
                await context.Response.WriteAsync("Invalid Api Key");
                return;
            }
        
            await _next(context);
        }
    }
}
