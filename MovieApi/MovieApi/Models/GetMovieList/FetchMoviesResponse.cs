namespace MovieApi.Models.GetMovieList
{
    public class FetchMoviesResponse
    {
        public List<MovieSummary> Movies { get; set; } = new List<MovieSummary>();
    }
}
