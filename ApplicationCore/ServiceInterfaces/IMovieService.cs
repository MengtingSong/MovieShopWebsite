using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetAllMovies();
        Task<List<MovieCardResponseModel>> GetTop30RatedMovies();
        Task<List<MovieCardResponseModel>> GetTop30RevenueMovies();
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<List<MovieReviewResponseModel>> GetMovieReviews(int id);
        Task<IEnumerable<CastResponseModel>> GetMovieCast(int id);
        Task<IEnumerable<GenreResponseModel>> GetMovieGenres(int id);
        Task<IEnumerable<GenreResponseModel>> GetAllGenres();

    }
}