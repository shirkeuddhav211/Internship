// HideHeadersMiddleware.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GISApi.Services
{
    public class HideHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public HideHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Remove("Request Method");
                httpContext.Request.Headers.Remove("Request Method");
                //httpContext.Response.Headers.Remove("HeaderName2");

                return Task.CompletedTask;
            }, context);

            await _next(context);
        }
    }
}
