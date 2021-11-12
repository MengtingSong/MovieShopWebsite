using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetTop30RatedMovies()
        {
            // var movieGroup = await _dbContext.Movies.Include(m => m.Reviews)
            //     .GroupBy(m => m.Id).Select(g => new
            //     {
            //         g = g, Rating = g.Select(m => m.Reviews.Average(r => r == null? 0 : r.Rating))
            //     }).OrderByDescending(g => g.Rating).Take(30).ToListAsync();
            // var movies = movieGroup.SelectMany(g => g.g);

            var movies = await _dbContext.Reviews.Include(r => r.Movie)
                .GroupBy(r => new { Id = r.MovieId, Title = r.Movie.Title, r.Movie.PosterUrl })
                .OrderByDescending(g => g.Average(r => r.Rating == null ? 0 : r.Rating))
                .Select(g =>
                    new Movie
                    {
                        Id = g.Key.Id,
                        Title = g.Key.Title,
                        PosterUrl = g.Key.PosterUrl,
                        Rating = g.Average(r => r.Rating == null ? 0 : r.Rating)
                    }).Take(30).ToListAsync();
            return movies;
        }
        
        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<MovieCast>> GetMovieCast(int id)
        {
            var movieCast = await _dbContext.MovieCasts.Where(mg => mg.MovieId == id)
                .Include(mg => mg.Cast).ToListAsync();
            return movieCast;
        }

        public async Task<IEnumerable<MovieGenre>> GetMovieGenres(int id)
        {
            // var movieGenre = await _dbContext.Movies.Where(m => m.Id == id)
            //     .Include(m => m.Genres).ThenInclude(mg => mg.Genre)
            //     .ToListAsync();
            
            var movieGenres = await _dbContext.MovieGenres.Where(mg => mg.MovieId == id)
                .Include(mg => mg.Genre).ToListAsync();
            
            // use Linq Join
            // var genres = await
            //     from mGenre in _dbContext.MovieGenres join genre in _dbContext.Genres on mGenre.GenreId equals genre.Id
            //     where mGenre.MovieId == id select genre.Name;
            
            return movieGenres;
        }

        public new async Task<Movie> GetById(int id)
        {
            var movie = await _dbContext.Movies
                .Include(m => m.Casts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres).ThenInclude(m => m.Genre)
                .Include(m => m.Trailers)
                .Include(m => m.Reviews).ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return null;
                // throw new Exception("Movie Not found");

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            if (movieRating > 0) movie.Rating = movieRating;

            return movie;
        }
        
        public async Task<IEnumerable<Review>> GetMovieReviews(int movieId, int pageSize = 30, int page = 1)
        {
            var reviews = await _dbContext.Reviews
                .Where(r => r.MovieId == movieId).Include(r => r.User).ToListAsync();
            return reviews;
        }
        
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            var genres = await _dbContext.Genres.Distinct().ToListAsync();
            return genres;
        }
    }
}