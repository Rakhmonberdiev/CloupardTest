
using API.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware 
    {
        private readonly ILogging _log;
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(ILogging logging, RequestDelegate next)
        {

            _log = logging;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context )
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _log.Log(ex.Message, "error");
            }
        }
    }
}
