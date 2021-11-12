using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetTop30RatedMovies();
        Task<IEnumerable<Movie>> GetTop30RevenueMovies();
        Task<IEnumerable<MovieCast>> GetMovieCast(int id);
        Task<IEnumerable<MovieGenre>> GetMovieGenres(int id);
        Task<IEnumerable<Review>> GetMovieReviews(int movieId, int pageSize = 30, int page = 1);
        Task<IEnumerable<Genre>> GetAllGenres();
    }
}