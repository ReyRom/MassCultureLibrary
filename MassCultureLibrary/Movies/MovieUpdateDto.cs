namespace MassCultureLibrary.Movies
{
    public class MovieUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int? ReleaseYear { get; set; }
    }
}