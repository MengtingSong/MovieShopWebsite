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
        [Route("")]
        // https://localhost/api/movies
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            if (!movies.Any()) return NotFound("No Movies Found");
            return Ok(movies);
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

        [HttpGet]
        [Route("toprated")]
        //https:localhost/api/movies/toprated
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTop30RatedMovies();
            if (!movies.Any()) return NotFound($"No movies found");
            return Ok(movies);
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
        
        [HttpGet]
        [Route("genre/{genreId:int}")]
        // http://localhost:5001/api/movies/genre/5?pageSize=30&pageIndex=35
        // many movies belonging to a genre => pagination 
        // 2000 movies for movieId 5
        // 30 movies per page => show how many page number
        // 2000/30 => 67 pages
        public async Task<IActionResult> GetMoviesByGenres(int genreId, [FromQuery] int pageSize = 30, [FromQuery] int pageIndex = 1)
        {
            // click on Page 1: movies 1 to 30
            // click on page 2: 31 to 60
            // click on Page 3: 61 to 90
            // LINQ moviegenres.skip(pageindex-1).take(pagesize).tolistasync()
            // SQL: offset 0 and fetch next 30
            // server-side pagination (is always preferred)

            
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReviews(int id)
        {
            var reviews = await _movieService.GetMovieReviews(id);
            if (!reviews.Any()) return NotFound($"No movie reviews found for this {id}");
            return Ok(reviews);
        }
    }
}