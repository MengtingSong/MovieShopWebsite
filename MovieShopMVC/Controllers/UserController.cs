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

        // TODO: Add purchase get method - details page
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
            var isSuccessPurchase = await _userService.PurchaseMovie(requestModel, userId);
            if (isSuccessPurchase)
            {
                // TODO: show messages telling user whether the movie successfully purchased
                return RedirectToAction("Purchases");
            }

            // TODO: show error message of purchase failure
            return View("../Views/Movies/Details");
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
        
        // TODO: Add/Remove favorite frontend - movie details page && favorite list page
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
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUserService.UserId;
            var favorites = await _userService.GetAllFavoritesForUser(userId);
            return View(favorites);
        }

        // TODO: Add/Edit/Delete review frontend - movie details page && review list page
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Review()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Review(ReviewRequestModel requestModel)
        {
            requestModel.UserId = _currentUserService.UserId;
            await _userService.AddMovieReview(requestModel);
            return RedirectToAction("Reviews");
        }

        // TODO: List of reviews for user page
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