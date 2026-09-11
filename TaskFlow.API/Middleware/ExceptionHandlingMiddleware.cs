using Microsoft.AspNetCore.Http;

namespace TaskFlow.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) {

            try
            {
                await _next(context);
            }

            catch (ArgumentException ex)
            {
                await WriteError(context, 400, ex.Message);
            }

            catch (InvalidOperationException ex) { 
            
                await WriteError(context, 400, ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");
                await WriteError(context, 500, "Ann unexpected error occured");
            }
        }

        private static async Task WriteError(HttpContext context, int statusCode, string statusMessage)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { error = statusMessage });
        }

    }
}
