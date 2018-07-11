using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace SecuringAPIWithCookie
{
    public class EndpointMiddleware
    {
        private readonly RequestDelegate _nextRequestDelegate;

        public EndpointMiddleware(RequestDelegate nextRequestDelegate)
        {
            _nextRequestDelegate = nextRequestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Equals("/authorize/policies"))
            {
                if (!HttpMethods.IsGet(httpContext.Request.Method))
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    return;
                }

                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    var request = httpContext.Request;
                    var redirectUrl = request.Query["redirecturl"];

                    var returnUrl = $"{request.PathBase.Value}/authorize/policies?redirecturl={redirectUrl}";
                    var loginUrl = $"/account/login?returnUrl={returnUrl}";

                    httpContext.Response.Redirect($"{request.Scheme}://{request.Host.ToUriComponent()}{loginUrl}");
                    return; 
                }

                await (new AuthorizeEndpoint()).Process(httpContext);
                return;
            }

            await _nextRequestDelegate(httpContext);
        }
    }
}
