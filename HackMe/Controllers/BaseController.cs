using HackMe.Application.Enums;
using HackMe.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly IChallengeTaskService _challengeTaskService;

        public BaseController(IChallengeTaskService challengeTaskService)
        {
            _challengeTaskService = challengeTaskService;
        }

        protected string GetUserIdentity()
        {
            var userIdentity = User?.Identity?.Name;
            if (userIdentity == null)
            {
                this.RedirectToAction("Index", "Home");
            }

            return userIdentity;
        }

        protected async Task CreateChallengeResult(ChallengeTaskType type)
        {
            var successful = await _challengeTaskService.CreateResult(GetUserIdentity(), (int)type);
            if (successful)
            {
                SetBanner(successful);
            }
        }

        protected void SetBanner(bool value)
        {
            ViewBag.ShowBanner = value;
        }
    }
}
