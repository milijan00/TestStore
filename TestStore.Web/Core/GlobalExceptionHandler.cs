using TestStore.Implementation.Exceptions;
using System.Linq;
namespace TestStore.Web.Core
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        //private readonly IExceptionLogger _logger;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception ex)
            {
                //_logger.Log(ex);

                httpContext.Response.ContentType = "application/json";
                object response = null;
                var statusCode = StatusCodes.Status500InternalServerError;

                if (ex is ForbiddenUsecaseExecutionException)
                {
                    statusCode = StatusCodes.Status403Forbidden;
                }

                if (ex is EntityNotFoundException)
                {
                    statusCode = StatusCodes.Status404NotFound;
                }

                if (ex is UnprocessableEntityException e)
                {
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    response = e.Errors;
                    //response = new
                    //{
                    //    errors = e.Errors.Select(x => new
                    //    {
                    //        property = x.PropertyName,
                    //        error = x.ErrorMessage
                    //    })
                    //};
                }

                //if (ex is UseCaseConflictException conflictEx)
                //{
                //    statusCode = StatusCodes.Status409Conflict;
                //    response = new { message = conflictEx.Message };
                //}


                httpContext.Response.StatusCode = statusCode;
                if (response != null)
                {
                    await httpContext.Response.WriteAsJsonAsync(response);
                }
            }
        }

    }
}
