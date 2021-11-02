using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // create an api method that shows top 30 revenue/grossing movies
        // so that my SPA, iOS and Android app show those movies in the home screen

        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet]
        [Route("{id:int}")]
        // https://localhost/api/movies/{id}
        public async Task<IActionResult> GetMovieDetails(int id)
        {
            var movieDetails = await _movieService.GetMovieDetails(id);
            if (movieDetails == null) return NotFound($"No movie found for this {id}");
            return Ok(movieDetails);
        }

        // create the api method that shows top 30 movies , json data

        [HttpGet]
        [Route("toprevenue")]
        // Attribute based routing
        // https://localhost/api/movies/toprevenue
        // API
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop30RevenueMovies();

            // return JSON data and Http Status Code //
            
            if (!movies.Any())
            {
                // return 404
                return NotFound("No Movies Found");
            }

            // 200 OK
            return Ok(movies);

            // for converting C# objects to Json objects there are 2 ways
            // before .NET Core 3, we used NewtonSoft.Json library
            // Microsoft created their own JSON Serialization library
            // System.Text.Json

        }
    }
}