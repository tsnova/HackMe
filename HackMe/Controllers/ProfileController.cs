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

        public ProfileController(
            IAgentService agentService,
            IChallengeTaskService challengeTaskService,
            IMapper mapper) : base(challengeTaskService)
        {
            _agentService = agentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string codeName, bool? showBanner)
        {
            var agent = await _agentService.GetAgent(codeName);

            if (agent == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            var authorized = agent.CodeName == GetUserIdentity();
            if (!authorized)
            {
                await CreateChallengeResult(ChallengeTaskType.UnauthorizedProfile);
            }

            var viewModel = _mapper.Map<AgentViewModel>(agent);
            viewModel.CanUpdate = authorized;
            SetBanner(showBanner ?? false);
            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string codeName, string activeMission)
        {
            if (String.IsNullOrEmpty(codeName))
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            var showBanner = false;
            var identity = GetUserIdentity();

            var authorized = codeName == identity;
            if (!authorized)
            {
                showBanner = await CreateChallengeResult(ChallengeTaskType.UpdateOthersProfile);
            }

            await _agentService.UpdateAgentActiveMission(codeName, activeMission);

            var result = await CheckPotentialXSS(codeName, activeMission);
            showBanner = showBanner ? showBanner : result;

            return RedirectToAction("Index", new { codeName, showBanner });
        }
    }
}
