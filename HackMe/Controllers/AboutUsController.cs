using HackMe.Application.Enums;
using HackMe.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    public class AboutUsController : BaseController
    {
        private readonly ILogger<AboutUsController> _logger;

        public AboutUsController(
            IChallengeTaskService challengeTaskService,
            ILogger<AboutUsController> logger) : base(challengeTaskService)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            await CreateChallengeResult(ChallengeTaskType.Login);
            return View();
        }
    }
}
