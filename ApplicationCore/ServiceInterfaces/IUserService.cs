using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel);
        Task<int> UpdateUser(UserRegisterRequestModel requestModel, int userId);
        Task AddFavorite(FavoriteRequestModel favoriteRequest);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<FavoriteResponseModel> GetAllFavoritesForUser(int id);
        Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
        Task<PurchaseResponseModel> GetAllPurchasesForUser(int id);
        Task<PurchaseDetailsResponseModel> GetPurchaseDetails(int userId, int movieId);
        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task DeleteMovieReview(int userId, int movieId);
        Task<ReviewResponseModel> GetAllReviewsByUser(int id);
    }
}