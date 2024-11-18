using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassCultureLibrary.Movies
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Task<Movie> AddMovieAsync(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieAsync(Guid movieId)
        {
            throw new NotImplementedException();
        }

        public Task<Movie?> GetMovieByIdAsync(Guid movieId)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateMovieAsync(Guid movieId, MovieUpdateDto updateInfo)
        {
            throw new NotImplementedException();
        }
    }
}
