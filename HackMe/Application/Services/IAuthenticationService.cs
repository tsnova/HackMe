namespace HackMe.Application.Services
{
    public interface IAuthenticationService
    {
        Task<bool> SignIn(string username, string password, HttpContext httpContext);
    }
}
