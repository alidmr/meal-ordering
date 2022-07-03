using MealOrdering.Shared.ResponseModels;
using Newtonsoft.Json;

namespace MealOrdering.Server.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static ILogger<ExceptionHandlingMiddleware> _loggerFactory;

        public ExceptionHandlingMiddleware(RequestDelegate Next, ILogger<ExceptionHandlingMiddleware> Logger)
        {
            _next = Next;
            _loggerFactory = Logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (!httpContext.Response.HasStarted)
                {
                    _loggerFactory.LogError(ex, "Request Error");

                    httpContext.Response.StatusCode = 200;
                    httpContext.Response.ContentType = "application/json";
                    var response = new ServiceResponse<String>();
                    response.SetException(ex);
                    var json = JsonConvert.SerializeObject(response);
                    await httpContext.Response.WriteAsync(json);
                }
            }
        }
    }
}
