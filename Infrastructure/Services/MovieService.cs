using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieCardResponseModel>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAll();
            if (movies == null) return null;
            
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl
                });
            }
            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetTop30RatedMovies()
        {
            var movies = await _movieRepository.GetTop30RatedMovies();
            if (movies == null) return null;
            
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl
                });
            }
            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetTop30RevenueMovies()
        {
            var movies = await _movieRepository.GetTop30RevenueMovies();
            if (movies == null) return null;
            
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl
                });
            }
            return movieCards;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null) return null;
            // {
            //     throw new Exception($"No movie found for this {id}");
            // }

            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id, Budget = movie.Budget, Overview = movie.Overview, Price = movie.Price,
                PosterUrl = movie.PosterUrl, Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(), Tagline = movie.Tagline,
                Title = movie.Title, RunTime = movie.RunTime, Rating = movie.Rating,
                BackdropUrl = movie.BackdropUrl, ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl
            };

            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(new GenreResponseModel
                {
                    Id = genre.GenreId, Name = genre.Genre.Name
                });
            }

            foreach (var cast in movie.Casts)
            {
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = cast.CastId, Character = cast.Character,
                    Name = cast.Cast.Name, ProfilePath = cast.Cast.ProfilePath
                });
            }

            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(new TrailerResponseModel
                {
                    Id = trailer.Id, Name = trailer.Name,
                    MovieId = trailer.MovieId, TrailerUrl = trailer.TrailerUrl
                });
            }

            foreach (var review in movie.Reviews)
            {
                movieDetails.Reviews.Add(new MovieReviewResponseModel
                {
                    UserId = review.UserId,
                    FirstName = review.User.FirstName,
                    LastName = review.User.LastName,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }
            return movieDetails;
        }

        public async Task<List<MovieReviewResponseModel>> GetMovieReviews(int id)
        {
            var reviews = await _movieRepository.GetMovieReviews(id);
            if (reviews == null) return null;
            var reviewList = new List<MovieReviewResponseModel>();
            foreach (var review in reviews)
            {
                reviewList.Add(new MovieReviewResponseModel
                {
                    FirstName = review.User.FirstName,
                    LastName = review.User.LastName,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText,
                    UserId = review.UserId
                });
            }

            return reviewList;
        }

        public async Task<IEnumerable<CastResponseModel>> GetMovieCast(int id)
        {
            var movieCast = await _movieRepository.GetMovieCast(id);
            if (movieCast == null) return null;
            var castList = new List<CastResponseModel>();
            foreach (var cast in movieCast)
            {
                castList.Add(new CastResponseModel
                {
                    Id = cast.CastId,
                    Name = cast.Cast.Name,
                    Gender = cast.Cast.Gender,
                    Character = cast.Character,
                    ProfilePath = cast.Cast.ProfilePath,
                    TmdbUrl = cast.Cast.TmdbUrl
                });
            }

            return castList;
        }

        public async Task<IEnumerable<GenreResponseModel>> GetMovieGenres(int id)
        {
            var genres = await _movieRepository.GetMovieGenres(id);
            if (genres == null) return null;
            var genreList = new List<GenreResponseModel>();
            foreach (var genre in genres)
            {
                genreList.Add(new GenreResponseModel
                {
                    Id = genre.GenreId,
                    Name = genre.Genre.Name
                });
            }

            return genreList;
        }

        public async Task<IEnumerable<GenreResponseModel>> GetAllGenres()
        {
            var genres = await _movieRepository.GetAllGenres();
            if (genres == null) return null;
            var genreList = new List<GenreResponseModel>();
            foreach (var genre in genres)
            {
                genreList.Add(new GenreResponseModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }
            return genreList;
        }
    }
}
