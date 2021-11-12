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
    public class CastController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public CastController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovieCast(int id)
        {
            var cast = await _movieService.GetMovieCast(id);
            if (!cast.Any()) return NotFound("No Movies Genres Found");
            return Ok(cast);
        }
    }
}