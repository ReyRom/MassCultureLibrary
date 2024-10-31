using FluentAssertions;
using MassCultureLibrary.Movies;
using Moq;
namespace MassCultureLibrary.Tests
{
    public class MovieTests
    {
        private readonly IMovieService _movieService;
        private readonly Movie _movie;
        public MovieTests()
        {
            var movieRepository = new Mock<IMovieRepository>();
            var movieService = new Mock<IMovieService>();
            _movieService = movieService.Object;
            _movie = new Movie { Id = Guid.NewGuid(), Title = "Матрица", Genre = "Фантастика", ReleaseYear = 1999 };
        }

        [Fact]
        public async Task AddMovie_ShouldAddMovieCorrectly()
        {
            var movie = _movie;

            var result = await _movieService.AddMovieAsync(movie);

            result.Should().NotBeNull();
            result.Title.Should().Be("Матрица");
        }

        [Fact]
        public async Task GetMovieById_ShouldReturnCorrectMovie()
        {
            var movieId = _movie.Id;
            var movie = await _movieService.GetMovieByIdAsync(movieId);

            movie.Should().NotBeNull();
            movie.Id.Should().Be(movieId);
        }

        [Fact]
        public async Task UpdateMovie_ShouldUpdateMovieDetails()
        {
            var movieId = _movie.Id;
            var updateInfo = new MovieUpdateDto { Title = "Матрица Революция" };

            var updatedMovie = await _movieService.UpdateMovieAsync(movieId, updateInfo);

            updatedMovie.Should().NotBeNull();
            updatedMovie.Title.Should().Be("Матрица Революция");
        }

        [Fact]
        public async Task DeleteMovie_ShouldRemoveMovie()
        {
            var movieId = _movie.Id;

            await _movieService.DeleteMovieAsync(movieId);

            var movie = await _movieService.GetMovieByIdAsync(movieId);
            movie.Should().BeNull();
        }
    }

}