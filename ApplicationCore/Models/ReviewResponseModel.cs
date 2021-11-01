using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class ReviewResponseModel
    {
        public ReviewResponseModel()
        {
            Reviews = new List<MovieReviewResponseModel>();
        }
        public int TotalReviewsCount { get; set; }
        public List<MovieReviewResponseModel> Reviews { get; set; }
    }

    public class MovieReviewResponseModel : MovieCardResponseModel
    {
        public string ReviewText { get; set; }
        public decimal Rating { get; set; }
    }
}