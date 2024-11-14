using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassCultureLibrary.Movies
{
    public interface IMovieRepository
    {
        Task<Movie> AddAsync(Movie movie);
        Task<Movie?> GetByIdAsync(Guid id);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(Guid id);
    }

}
