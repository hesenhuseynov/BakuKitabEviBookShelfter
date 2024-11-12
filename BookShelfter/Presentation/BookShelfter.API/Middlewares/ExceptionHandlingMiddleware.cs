using Newtonsoft.Json;
using System.Net;

namespace BookShelfter.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }



        private Task HandleExceptionAsync(HttpContext context, Exception exception )
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var errorMessage = "An unexpected error occured,Please try again later";

            switch (exception)
            {
                case KeyNotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    errorMessage = "The requested resource was not found.";
                    break;

                case UnauthorizedAccessException _:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorMessage = "You are not authorized to access this resource.";
                    break;

                case ArgumentException _:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = "The request contains invalid arguments.";
                    break;

                case InvalidOperationException _:
                    statusCode = HttpStatusCode.Conflict;
                    errorMessage = "The request could not be completed due to a conflict with the current state of the resource.";
                    break;

                case TimeoutException _:
                    statusCode = HttpStatusCode.RequestTimeout;
                    errorMessage = "The request has timed out.";
                    break;

                case NotImplementedException _:
                    statusCode = HttpStatusCode.NotImplemented;
                    errorMessage = "This functionality is not yet implemented.";
                    break;

                case Exception _:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorMessage = "An unexpected error occurred. Please try again later.";
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorMessage = "An unknown error occurred.";
                    break;
            }
            _logger.LogError(exception, exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonConvert.SerializeObject(new
            {
                error = errorMessage,
                detail = exception.Message
            });

            return context.Response.WriteAsync(result);


        }
    }
}
