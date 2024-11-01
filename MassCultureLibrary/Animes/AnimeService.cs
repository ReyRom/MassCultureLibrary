using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassCultureLibrary.Animes
{
    public class AnimeService : IAnimeService
    {
        IAnimeRepository _repository;

        public AnimeService(IAnimeRepository repository)
        {
            _repository = repository;
        }

        public Task<Anime> AddAnimeAsync(Anime anime)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAnimeAsync(Guid animeId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Anime>> GetAnimeAsync()
        {
            return await _repository.GetAnimeAsync();
        }

        public Task<Anime?> GetAnimeByIdAsync(Guid animeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Anime>> GetAnimeByStatusAsync(string status)
        {
            throw new NotImplementedException();
        }

        public Task<Anime> UpdateAnimeAsync(Guid animeId, AnimeUpdateDto updateInfo)
        {
            throw new NotImplementedException();
        }
    }
}
