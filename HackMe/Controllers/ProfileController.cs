using AutoMapper;
using HackMe.Application.Services;
using HackMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IAgentService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(
            IAgentService service,
            IMapper mapper,
            ILogger<ProfileController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string codeName)
        {
            var agent = await _service.GetAgent(codeName);

            if (agent == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            var viewModel = _mapper.Map<AgentViewModel>(agent);
            return View(viewModel);
        }
    }
}
