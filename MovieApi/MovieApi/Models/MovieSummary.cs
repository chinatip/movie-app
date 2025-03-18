namespace MovieApi.Models
{
    public class MovieSummary
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }

        public List<MovieProvider> Providers { get; set; } = new();
    }
}
