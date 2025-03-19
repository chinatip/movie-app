using Microsoft.AspNetCore.Mvc;
using MovieApi.Models.GetMovieList;

namespace MovieApi.Models.GetMovieDetail
{
    public class GetMovieDetailRequest
    {
        public List<MovieSourceInfo> MovieSourceInfos { get; set; }
    }

    public class MovieSourceInfo
    {
        public MovieProvider ProviderID { get; set; }
        public string MovieID { get; set; }
    }
}