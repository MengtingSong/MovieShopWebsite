using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetTop30RevenueMovies();
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<IEnumerable<string>> GetGenres();
    }
}