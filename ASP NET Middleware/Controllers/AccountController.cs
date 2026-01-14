namespace ASP_NET_Middleware.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using ASP_NET_Middleware.Models;
    using ASP_NET_Middleware.Services;

    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View(new LoginModel());

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model, string? returnUrl)
        {
            if (await _userService.ValidateCredentialsAsync(model.Username, model.Password))
            {
                var principal = await _userService.CreatePrincipalAsync(model.Username);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid credentials");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}