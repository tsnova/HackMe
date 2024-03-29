using AutoMapper;
using HackMe.Application.Enums;
using HackMe.Application.Services;
using HackMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    public class MissionsController : BaseController
    {
        private readonly IAgentService _agentService;
        private readonly IMapper _mapper;

        public MissionsController(
            IAgentService agentService,
            IChallengeTaskService challengeTaskService,
            IMapper mapper) : base(challengeTaskService)
        {
            _agentService = agentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var classified = await GetClassifiedSetting(false);
            var results = _agentService.GetMissionsList(string.Empty, classified);

            var viewModel = _mapper.Map<IEnumerable<MissionViewModel>>(results);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string searchKey)
        {
            var classified = await GetClassifiedSetting(false);
            var results = _agentService.GetMissionsList(searchKey, classified);

            var viewModel = _mapper.Map<IEnumerable<MissionViewModel>>(results);
            return View("Index", viewModel);
        }

        public async Task<IActionResult> Detail(int id, bool? showBanner)
        {
            var classified = await GetClassifiedSetting(true);
            var result = _agentService.GetMissionDetails(GetUserIdentity(), id);

            if (result == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            if (result.IsClassified != classified)
            {
                return RedirectToAction("UnAuthorized", "Error");
            }

            if (showBanner == true)
            {
                SetBanner(true);
            }

            var viewModel = _mapper.Map<MissionViewModel>(result);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment([FromRoute] int id, string comment)
        {
            var identity = GetUserIdentity();            
            var showBanner = await CheckPotentialXSS(comment);

            _agentService.CreateMissionComment(identity, id, comment);

            return RedirectToAction("Detail", new { id, showBanner });
        }

        private async Task<bool> GetClassifiedSetting(bool createResult)
        {
            var showClassifedDataCookie = HttpContext.Request.Cookies["showClassifiedData"];

            if (bool.TryParse(showClassifedDataCookie, out var showClassifedData) && showClassifedData && createResult)
            {
                await CreateChallengeResult(ChallengeTaskType.ShowClassifiedData);
            }

            return showClassifedData;
        }
    }
}
