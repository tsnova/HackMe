using HackMe.Application.Enums;
using HackMe.Application.Helpers;
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

        protected async Task<bool> CreateChallengeResult(ChallengeTaskType type, string? input = null)
        {
            var successful = await _challengeTaskService.CreateResult(GetUserIdentity(), (int)type, input);
            if (successful)
            {
                SetBanner(successful);
            }

            return successful;
        }

        protected void SetBanner(bool value)
        {
            ViewBag.ShowBanner = value;
        }

        protected async Task<bool> CheckPotentialXSSAndCreateResult(params string[] values)
        {
            foreach (var value in values)
            {
                if (InputHelper.HasPotentialXss(value))
                {
                    var success = await CreateChallengeResult(ChallengeTaskType.XSSAttack);
                    return success;
                }
            }

            return false;
        }
    }
}
