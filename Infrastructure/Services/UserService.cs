using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // check whether email exists in the database
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
                //email exists in the database
                throw new Exception("Email already exists, please login");

            // generate a random unique salt
            var salt = GetSalt();

            // create the hashed password with salt generated in the above step
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);

            // save the user object to db
            // create user entity object
            var user = new User
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = requestModel.DateOfBirth
            };
            
            // use EF to save this user in the user table
            var newUser = await _userRepository.Add(user);
            return newUser.Id;
        }

        private string GetSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }
        
        public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel)
        {
            // get the salt and hashed password from database for this user
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser == null) throw null;

            // hash the user entered password with salt from the database
            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);
            // check the hashed password with database hashed password
            // TODO: uncomment for official running
            // if (hashedPassword != dbUser.HashedPassword) return null;
            
            // user entered correct password
            var userLoginResponseModel = new UserLoginResponseModel
            {
                Id = dbUser.Id,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault(),
                Email = dbUser.Email
            };
            return userLoginResponseModel;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchase = new Purchase
            {
                PurchaseNumber = purchaseRequest.PurchaseNumber,
                PurchaseDateTime = purchaseRequest.PurchaseDateTime,
                MovieId = purchaseRequest.MovieId,
                UserId = userId
            };
            var newPurchase = await _purchaseRepository.Add(purchase);
            return newPurchase != null;
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchase = await _purchaseRepository.GetPurchaseDetails(userId, purchaseRequest.MovieId);
            return  purchase != null;
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            var purchases = await _purchaseRepository.GetAllPurchasesForUser(id);
            var purchasedMovies = new List<MovieCardResponseModel>();
            foreach (var purchase in purchases)
            {
                var movieCard = new MovieCardResponseModel
                {
                    Id = purchase.MovieId,
                    Title = purchase.Movie.Title,
                    PosterUrl = purchase.Movie.PosterUrl
                };
                purchasedMovies.Add(movieCard);
                // Why can't use Add method to list property?
                // purchaseResponseModel.PurchasedMovies.Add(movieCard);
            }

            var purchaseResponseModel = new PurchaseResponseModel
            {
                PurchasedMovies = purchasedMovies,
                TotalMoviesPurchased = purchasedMovies.Count
            };
            
            return purchaseResponseModel;
        }

        public async Task<PurchaseDetailsResponseModel> GetPurchaseDetails(int userId, int movieId)
        {
            var purchase = await _purchaseRepository.GetPurchaseDetails(userId, movieId);
            var purchaseDetails = new PurchaseDetailsResponseModel
            {
                Id = purchase.Id,
                MovieId = purchase.MovieId,
                PosterUrl = purchase.Movie.PosterUrl,
                PurchaseDateTime = purchase.PurchaseDateTime,
                PurchaseNumber = purchase.PurchaseNumber,
                ReleaseDate = purchase.Movie.ReleaseDate,
                Title = purchase.Movie.Title,
                TotalPrice = purchase.TotalPrice,
                UserId = purchase.UserId
            };
            return purchaseDetails;
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<ReviewResponseModel> GetAllReviewsByUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAccount(UserRegisterRequestModel requestModel, int id)
        {
            var salt = GetSalt();
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);
            var user = new User
            {
                Id = id,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                DateOfBirth = requestModel.DateOfBirth
            };
            var updatedUser = await _userRepository.Update(user);
            return updatedUser.Id;
        }
    }
}