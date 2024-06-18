namespace API.Authentication
{
    public class ApiKeyAuthMiddleWare
    {
        private readonly RequestDelegate _next;

        public ApiKeyAuthMiddleWare (RequestDelegate next, IConfiguration config)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var apiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key Missing!");
                return;
            }
            var _apiKey = AuthConstants.ApiKeyHeaderValue;
            if (!_apiKey.Equals(apiKey)) 
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key Validation Failed!");
                return;
            }
            await _next(context);
        }

        
        
    }

}
