using System;
using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class MovieDetailsResponseModel
    {
        public MovieDetailsResponseModel()
        {
            Casts = new List<CastResponseModel>();
            Genres = new List<GenreResponseModel>();
            Trailers = new List<TrailerResponseModel>();
            Reviews = new List<ReviewDetailsResponseModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string BackdropUrl { get; set; }
        public string Overview { get; set; }
        public string Tagline { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Revenue { get; set; }
        public string ImdbUrl { get; set; }
        public string TmdbUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? RunTime { get; set; }
        public decimal? Price { get; set; }
        public decimal Rating { get; set; }
        
        public List<CastResponseModel> Casts { get; set; }
        public List<GenreResponseModel> Genres { get; set; }
        public List<TrailerResponseModel> Trailers { get; set; }
        public List<ReviewDetailsResponseModel> Reviews { get; set; }
    }
    
    
    public class CastResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; }
        public string Character { get; set; }
    }
    public class GenreResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TrailerResponseModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }
    }
}
