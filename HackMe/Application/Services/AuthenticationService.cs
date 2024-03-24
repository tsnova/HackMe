using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace HackMe.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<bool> SignIn(string username, string password, HttpContext httpContext)
        {
            var successful = true;
            if (successful)
            {
                await AuthenticateUser(username, httpContext);
            }

            return successful;
        }

        private static async Task AuthenticateUser(string username, HttpContext httpContext)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, username),
            };

            var identity = new ClaimsIdentity(claims, "customAuthentication");
            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync("customAuthentication", principal);

            // Set session value
            httpContext.Session.SetString("codeName", username);
            httpContext.Response.Cookies.Append("showClassifedData", true.ToString());
        }
    }
}
