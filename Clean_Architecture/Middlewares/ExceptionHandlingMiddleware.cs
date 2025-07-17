using Clean_Architecture.Applicaiton.Common.Exceptions;
using Clean_Architecture.Share.ApiResponse;
using System.Net;

namespace Clean_Architecture.API.Middlewares
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex) // custom ValidationException
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = RESTfulAPIResponse<object>.FailResponse(
                       message: "Validation failed",
                       errors: ex.Errors
                   );
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (FluentValidation.ValidationException ex) // from FluentValidation
            {
                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = RESTfulAPIResponse<object>.FailResponse(
                       message: "Validation failed",
                       errors: ex.Errors
                );
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (KeyNotFoundException ex) // not found data
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                var response = RESTfulAPIResponse<object>.FailResponse(
                       message: "Validation failed",
                       errors: ex.Message
                );
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = RESTfulAPIResponse<object>.FailResponse(
                    message: "Có lỗi xảy ra trong hệ thống. Vui lòng thử lại sau.",
                    errors: "Server error"
                );

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
