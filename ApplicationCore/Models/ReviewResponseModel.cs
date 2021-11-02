using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class ReviewResponseModel
    {
        public ReviewResponseModel()
        {
            Reviews = new List<UserReviewResponseModel>();
        }
        public int TotalReviewsCount { get; set; }
        public List<UserReviewResponseModel> Reviews { get; set; }
    }

    public class UserReviewResponseModel : MovieCardResponseModel
    {
        public string ReviewText { get; set; }
        public decimal Rating { get; set; }
    }
}