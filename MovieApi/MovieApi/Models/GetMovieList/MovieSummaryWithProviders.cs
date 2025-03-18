﻿namespace MovieApi.Models.GetMovieList
{
    public class MovieSummaryWithProviders
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }

        public List<ProviderDetail> ProviderDetails { get; set; }
    }

    public class ProviderDetail
    {
        public string ProviderName { get; set; }
        public MovieProvider ProviderID { get; set; }
        public string MovieID { get; set; }
    }
}
