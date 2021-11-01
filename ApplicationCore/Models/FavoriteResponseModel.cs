using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class FavoriteResponseModel
    {        
        public int TotalMoviesFavored { get; set; }
        public List<FavoriteMovieResponseModel> FavoriteMovies { get; set; }

        public class FavoriteMovieResponseModel : MovieCardResponseModel
        {
        }
    }
}