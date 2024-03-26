using AutoMapper;
using HackMe.Application.Enums;
using HackMe.Application.Services;
using HackMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    public class NewsController : BaseController
    {
        private readonly IAgentService _agentService;
        private readonly IMapper _mapper;

        public NewsController(
            IAgentService agentService,
            IChallengeTaskService challengeTaskService,
            IMapper mapper) : base(challengeTaskService)
        {
            _agentService = agentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var classified = await GetClassifiedSetting();
            var results = _agentService.GetNewsList(classified);

            var viewModel = _mapper.Map<IEnumerable<NewsViewModel>>(results);
            return View(viewModel);
        }

        public async Task<IActionResult> NewsDetail(int id)
        {
            var classified = await GetClassifiedSetting();
            var result = _agentService.GetNewsItem(id);

            if (result == null || result.IsClassified != classified)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            if (result.IsClassified != classified)
            {
                return RedirectToAction("UnAuthorized", "Error");
            }

            var viewModel = _mapper.Map<NewsViewModel>(result);
            return View(viewModel);
        }

        private async Task<bool> GetClassifiedSetting()
        {
            var showClassifedDataCookie = HttpContext.Request.Cookies["showClassifedData"];

            if (bool.TryParse(showClassifedDataCookie, out var showClassifedData) && showClassifedData)
            {
                await CreateChallengeResult(ChallengeTaskType.ShowClassifiedData);
            }

            return showClassifedData;
        }
    }
}
