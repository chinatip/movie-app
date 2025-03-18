using MovieApi.Models;

namespace MovieApi.Helpers
{
    public static class ProviderHelper
    {
        public static string GetProviderName(MovieProvider provider)
        {
            return provider switch
            {
                MovieProvider.CinemaWorld => "cinemaworld",
                MovieProvider.FilmWorld => "filmworld",
                _ => throw new ArgumentException("Unknown provider", nameof(provider))
            };
        }
    }
}
