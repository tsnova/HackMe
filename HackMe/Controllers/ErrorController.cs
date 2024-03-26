using HackMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult Unhandled()
        {
            return View(new ErrorViewModel());
        }

        public IActionResult UnAuthorized()
        {
            return View();
        }
    }
}
