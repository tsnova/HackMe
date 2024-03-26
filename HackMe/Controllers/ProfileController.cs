using AutoMapper;
using HackMe.Application.Enums;
using HackMe.Application.Services;
using HackMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IAgentService _agentService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(
            IAgentService agentService,
            IChallengeTaskService challengeTaskService,
            IMapper mapper,
            ILogger<ProfileController> logger) : base(challengeTaskService)
        {
            _agentService = agentService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string codeName)
        {
            var agent = await _agentService.GetAgent(codeName);

            if (agent == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            var userIdentityName = GetUserIdentity();
            if (agent.CodeName != userIdentityName)
            {
                await CreateChallengeResult(ChallengeTaskType.UnauthorizedProfile);
            }

            var viewModel = _mapper.Map<AgentViewModel>(agent);
            return View(viewModel);
        }
    }
}
