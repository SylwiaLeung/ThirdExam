using CommonItems.Exceptions;

namespace CrimeService.Behaviours
{
    public class ErrorHandlingBehaviour : IMiddleware
    {
        private ILogger<ErrorHandlingBehaviour> _logger;

        public ErrorHandlingBehaviour(ILogger<ErrorHandlingBehaviour> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequestException)
            {
                _logger.LogError(badRequestException, badRequestException.Message);

                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException, notFoundException.Message);

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (ApplicationException e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(("internalServerError"));
            }
        }
    }
}
