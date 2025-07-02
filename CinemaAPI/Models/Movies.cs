namespace CinemaAPI.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string? Date { get; set; }
    }
}
