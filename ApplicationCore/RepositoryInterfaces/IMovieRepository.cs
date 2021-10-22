using System.Collections.Generic;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetTop30RevenueMovies();
    }
}