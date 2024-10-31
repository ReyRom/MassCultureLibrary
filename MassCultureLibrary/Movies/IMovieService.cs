namespace MassCultureLibrary.Movies
{
    public interface IMovieService
    {
        Task<Movie> AddMovieAsync(Movie movie);
        Task DeleteMovieAsync(Guid movieId);
        Task<Movie?> GetMovieByIdAsync(Guid movieId);
        Task<Movie> UpdateMovieAsync(Guid movieId, MovieUpdateDto updateInfo);
    }
}