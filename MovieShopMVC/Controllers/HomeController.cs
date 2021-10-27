using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // Routing
        // https://localhost/home/index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movieCards = await _movieService.GetTop30RevenueMovies();
            // ViewBag.PageTitle = "Top Revenue Movies";
            // ViewData["test"] = "Test Data is Passed using ViewData";
            return View(movieCards);
        }

        // https://localhost/home/privacy
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
