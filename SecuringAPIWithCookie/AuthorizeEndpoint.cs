using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace SecuringAPIWithCookie
{
    public class AuthorizationData
    {
        public string Name { get; set; }
        public List<string> Policies { get; set; }
    }

    public class AuthorizeEndpoint
    {

        public async Task Process(HttpContext httpContext)
        {
            var redirectUrl = httpContext.Request.Query["redirectUrl"];
            var roleClaims = httpContext.User.Claims.Where(_ => _.Type == ClaimTypes.Role).Select(_ => _.Value);
            
            var authorizationData = new AuthorizationData()
            {
                Name = httpContext.User.Identity.Name,
                Policies = roleClaims.ToList()
            };

            string data = JsonConvert.SerializeObject(authorizationData);
            httpContext.Response.Redirect($"{redirectUrl}#{Convert.ToBase64String(Encoding.UTF8.GetBytes(data))}");

            await Task.CompletedTask;
            return;
        }

    }
}
