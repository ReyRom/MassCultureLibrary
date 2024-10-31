namespace MassCultureLibrary.Animes
{
    public class Anime
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Status { get; set; } = "Planned"; // Например, статус "Watching", "Completed", "Planned"
    }
}