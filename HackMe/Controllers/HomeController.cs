using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.ShowHeader = false;
            return View();
        }
    }
}
