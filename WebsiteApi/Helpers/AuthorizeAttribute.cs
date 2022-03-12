using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Helpers
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string[] Args { get; }
        public AuthorizeAttribute(params string[] args)
        {
            Args = args;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
           
            var user = (User)context.HttpContext.Items["User"];
            if (user != null)
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
