using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected string GetUserIdentity()
        {
            var userIdentity = User?.Identity?.Name;
            if (userIdentity == null)
            {
                this.RedirectToAction("Index", "Home");
            }

            return userIdentity;
        }
    }
}
