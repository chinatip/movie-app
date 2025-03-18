namespace MovieApi.Models
{
    public class MovieResponse
    {
        public List<MovieSummary> Movies { get; set; } = new List<MovieSummary>();
    }
}
