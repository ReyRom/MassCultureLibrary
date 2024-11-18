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
            => _repository.AddAsync(anime);

        public Task DeleteAnimeAsync(Guid animeId) 
            => _repository.DeleteAsync(animeId);

        public async Task<IEnumerable<Anime>> GetAnimeAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<Anime?> GetAnimeByIdAsync(Guid animeId) 
            => _repository.GetByIdAsync(animeId);

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
