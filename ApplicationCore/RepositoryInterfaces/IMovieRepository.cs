using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetTop30RevenueMovies();
        Task<IEnumerable<Review>> GetAllReviewsForMovie(int id, int pageSize = 30, int page = 1);
        Task<IEnumerable<string>> GetGenres();
    }
}