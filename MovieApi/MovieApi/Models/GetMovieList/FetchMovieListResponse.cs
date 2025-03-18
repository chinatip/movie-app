namespace MovieApi.Models.GetMovieList
{
    public class FetchMovieListResponse
    {
        public List<MovieSummary> Movies { get; set; } = new List<MovieSummary>();
    }
}
