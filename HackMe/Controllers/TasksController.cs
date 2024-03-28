using AutoMapper;
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
            var result = await _challengeTaskService.GetAll(GetUserIdentity());
            var viewModel = _mapper.Map<IEnumerable<ChallangeResultViewModel>>(result.Results);

            return View(viewModel);
        }
    }
}
