using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Purchase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Favorite()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Review()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {            
            // get the id from HttpContext.User.Claims
            var userIdentity = HttpContext.User.Identity;
            if (userIdentity is {IsAuthenticated: true})
            {
                // call the databsae to get the data
                var userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                // call _userService that will give list of moviesCard Models that this user purchased
                var purchases = await _userService.Purchases(userId);
                // Purchase, dbContext.Purchase.where(u=> u.UserId == id);
                return View(purchases);
            }
            
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            return View();
        }
    }
}