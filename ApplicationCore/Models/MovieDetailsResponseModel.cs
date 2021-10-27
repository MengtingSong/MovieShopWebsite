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
    }
}
