using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class PurchaseResponseModel
    {
        public int TotalMoviesPurchased { get; set; }
        public List<MovieCardResponseModel> PurchasedMovies { get; set; }
    }
}