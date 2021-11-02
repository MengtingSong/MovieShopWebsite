using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
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
        
        public async Task<IEnumerable<string>> GetGenres()
        {
            var genres = await _dbContext.Genres.Select(g => g.Name).Distinct().ToListAsync();
            return genres;
        }
    }
}