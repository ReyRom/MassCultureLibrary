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

        public async Task<Anime> AddAnimeAsync(Anime anime)
        {
            return await _repository.AddAsync(anime);
        }

        public async Task DeleteAnimeAsync(Guid animeId)
        {
            await _repository.DeleteAsync(animeId);
        }

        public async Task<IEnumerable<Anime>> GetAnimeAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Anime?> GetAnimeByIdAsync(Guid animeId)
        {
            return await _repository.GetByIdAsync(animeId);
        }

        public Task<IEnumerable<Anime>> GetAnimeByStatusAsync(string status)
        {
            throw new NotImplementedException();
        }

        public Task<Anime> UpdateAnimeAsync(Guid animeId, AnimeUpdateDto updateInfo)
        {
            throw new NotImplementedException();
        }
        public Task<Anime> GetAnimeByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task<List<Anime>> GetAnimesByGenreAsync(string genre)
        {
            throw new NotImplementedException();
        }
    }
}
