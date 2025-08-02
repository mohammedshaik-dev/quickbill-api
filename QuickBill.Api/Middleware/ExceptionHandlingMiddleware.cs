using log4net;
using QuickBill.Domain.Models.Common;
using System.Net;


namespace QuickBill.Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ExceptionHandlingMiddleware));

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext); // proceed
            }
            catch (Exception ex)
            {
                _logger.Error("Unhandled Exception", ex);

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var response = new ApiResponse<string>
                {
                    Status = "Error",
                    Code = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    Message = ex.Message,
                    Data = null,
                    Timestamp = DateTime.UtcNow
                };

                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
