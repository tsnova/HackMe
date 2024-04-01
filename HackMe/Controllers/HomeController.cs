using HackMe.Application.Services;
using HackMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace HackMe.Controllers
{
    public class HomeController : Controller
    {
        private const string ErrorLoginMessage = "Invalid username or password";

        private readonly IAuthenticationService _service;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IAuthenticationService service,
            ILogger<HomeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userIdentity = User?.Identity?.Name;
            if (userIdentity != null)
            {
                return this.RedirectToAction("Index", "AboutUs");
            }

            ViewBag.ShowHeader = false;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            ViewBag.ShowHeader = false;

            if (!ModelState.IsValid)
            {
                loginViewModel.ErrorMessage = ErrorLoginMessage;
                return View("Index", loginViewModel);
            }

            var success = await _service.SignIn(loginViewModel.CodeName, loginViewModel.Password, HttpContext);

            if (!success)
            {
                loginViewModel.ErrorMessage = ErrorLoginMessage;
                return View("Index", loginViewModel);
            }

            return RedirectToAction("Index", "AboutUs");
        }
    }
}
