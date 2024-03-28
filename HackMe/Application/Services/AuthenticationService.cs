using HackMe.Application.Helpers;
using HackMe.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace HackMe.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository _repository;

        public AuthenticationService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> SignIn(string username, string password, HttpContext httpContext)
        {
            var successful = false;
            if (await _repository.AgentExists(username))
            {
                password = Regex.Replace(password, @"\s+", " ").Trim();

                if (InputHelper.IsAllowedSqlInjection(password))
                {
                    var agent = _repository.ValidateAgentLogin(username, password);
                    successful = agent != null;
                }
                else
                {
                    var agent = await _repository.GetAgent(username, password);
                    successful = agent != null;
                }
            }

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

            httpContext.Session.SetString("codeName", username);
            httpContext.Response.Cookies.Delete("showClassifiedData");
            httpContext.Response.Cookies.Append("showClassifiedData", false.ToString());
        }
    }
}
