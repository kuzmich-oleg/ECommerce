using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Search.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                var error = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Status = 400,
                    Extensions = { ["traceId"] = context.TraceIdentifier },
                    Detail = ex.Message
                };
                
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
