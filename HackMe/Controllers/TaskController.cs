using HackMe.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    public class TaskController : BaseController
    {
        private readonly IChallengeTaskService _challengeTaskService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(IChallengeTaskService challengeTaskService,
            ILogger<TaskController> logger) : base(challengeTaskService)
        {
            _challengeTaskService = challengeTaskService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var results = await _challengeTaskService.GetAll(GetUserIdentity());
            return View(results);
        }
    }
}
