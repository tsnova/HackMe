using AutoMapper;
using HackMe.Application.Enums;
using HackMe.Application.Services;
using HackMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    public class TasksController : BaseController
    {
        private readonly IChallengeTaskService _challengeTaskService;
        private readonly IMapper _mapper;
        private readonly ILogger<TasksController> _logger;

        public TasksController(IChallengeTaskService challengeTaskService,
            IMapper mapper,
            ILogger<TasksController> logger) : base(challengeTaskService)
        {
            _challengeTaskService = challengeTaskService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await GetResults();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMissionsCounter(int counter)
        {            
            if (counter < 0)
            {
                return View("Index", await GetResults());
            }

            var success = await CreateChallengeResult(ChallengeTaskType.CountMissions, counter.ToString());
            var viewModel = await GetResults();

            if (!success && !viewModel.HasCompletedTask(ChallengeTaskType.CountMissions))
            {
                viewModel.ValidationMessage = "Wrong answer! please try again.";
            }

            return View("Index", viewModel);
        }

        private async Task<ChallangeResultsViewModel> GetResults()
        {
            var result = await _challengeTaskService.GetAll(GetUserIdentity());
            return new ChallangeResultsViewModel
            {
                Results = _mapper.Map<IEnumerable<ChallangeResultViewModel>>(result.Results)
            };
        }
    }
}
