using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    public class AboutUsController : BaseController
    {
        private readonly ILogger<AboutUsController> _logger;

        public AboutUsController(ILogger<AboutUsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
