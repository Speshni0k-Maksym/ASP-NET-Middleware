namespace ASP_NET_Middleware.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ASP_NET_Middleware.Services;

    public class WishesController : Controller
    {
        private readonly IWishService _wishService;

        public WishesController(IWishService wishService)
        {
            _wishService = wishService;
        }

        [HttpGet]
        public async Task<IActionResult> Write()
        {
            var wish = await _wishService.WriteRandomWishAsync();
            TempData["LastWish"] = wish;
            return RedirectToAction("Index", "Home");
        }
    }
}