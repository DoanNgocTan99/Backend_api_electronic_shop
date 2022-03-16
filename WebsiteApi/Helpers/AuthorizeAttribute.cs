using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Helpers
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; }
        public AuthorizeAttribute(string Roles)
        {
            this.Roles = Roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var token = string.Empty;
                if (token == null)
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ").Last();
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes("super secret key!");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var temp = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Typ).Value;
                if (!temp.Equals(this.Roles))
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
            catch (Exception)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
