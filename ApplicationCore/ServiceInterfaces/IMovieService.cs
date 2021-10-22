using System.Collections.Generic;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        List<MovieCardResponseModel> GetTop30RevenueMovies();
        
    }
}