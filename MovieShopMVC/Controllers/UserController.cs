using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private readonly ICurrentUserService _currentUserService;
        // private int userId;

        public UserController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
            // userId = _currentUserService.UserId;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Purchase()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Purchase(int movieId)
        {
            var userId = _currentUserService.UserId;
            var requestModel = new PurchaseRequestModel
            {
                MovieId = movieId
            };
            var isPurchased = await _userService.IsMoviePurchased(requestModel, userId);
            // TODO: show messages telling user movie already purchased
            if (isPurchased) return View("../Views/Movies/Details");
            var successPurchase = await _userService.PurchaseMovie(requestModel, userId);
            if (successPurchase)
            {
                // TODO: show messages telling user whether the movie successfully purchased
                return RedirectToAction("Purchases");
            }

            // TODO: show error message of purchase failure
            return View("../Views/Movies/Details");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Favorite(int movieId)
        {
            var favoriteRequestModel = new FavoriteRequestModel
            {
                UserId = _currentUserService.UserId,
                MovieId = movieId
            };
            await _userService.AddFavorite(favoriteRequestModel);
            return RedirectToAction("Favorites");
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Review(int movieId)
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Purchases()
        {
            // // get the id from HttpContext.User.Claims
            // var userIdentity = HttpContext.User.Identity;
            // if (userIdentity is {IsAuthenticated: true})
            // {
            //     // call the databsae to get the data
            //     var userId = Convert.ToInt32(HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //     // call _userService that will give list of moviesCard Models that this user purchased
            //     var purchases = await _userService.Purchases(userId);
            //     return View(purchases);
            // }
            //
            // return RedirectToAction("Login", "Account");

            // call the databsae to get the data
            var userId = _currentUserService.UserId;
            // call _userService that will give list of moviesCard Models that this user purchased
            var purchases = await _userService.GetAllPurchasesForUser(userId);
            return View(purchases.PurchasedMovies);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUserService.UserId;
            var favorites = await _userService.GetAllFavoritesForUser(userId);
            return View(favorites);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Reviews()
        {
            var userId = _currentUserService.UserId;
            var reviews = await _userService.GetAllReviewsByUser(userId);
            return View(reviews);
        }
    }
}