namespace MovieApi.Models
{
    public class MovieDetail
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string? Awards { get; set; }
        public string Poster { get; set; }
        public string Metascore { get; set; }
        public string Rating { get; set; }
        public string Votes { get; set; }
        public string Type { get; set; }
        public List<Price> Prices { get; set; }
    }

    public class Price
    {
        public MovieProvider Provider { get; set; }
        public string ProviderName { get; set; }
        public decimal Value { get; set; }
    }
}
