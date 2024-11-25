namespace MassCultureLibrary.Animes
{
    public class AnimeService : IAnimeService
    {
        IAnimeRepository _repository;
        public AnimeService(IAnimeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Anime> AddAnimeAsync(Anime anime) => await _repository.AddAsync(anime);

        public async Task DeleteAnimeAsync(Guid animeId) => await _repository.DeleteAsync(animeId);

        public async Task<IEnumerable<Anime>> GetAnimeAsync() => await _repository.GetAllAsync();

        public async Task<Anime?> GetAnimeByIdAsync(Guid animeId) => await _repository.GetByIdAsync(animeId);

        public async Task<IEnumerable<Anime>> GetAnimeByStatusAsync(string status)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> GetAnimeIdbyNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAnimeTitleById(Guid animeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Anime>> GetAnimeByGenreAsync(string status)
        {
            throw new NotImplementedException();
        }

        public async Task<Anime> UpdateAnimeAsync(Guid animeId, AnimeUpdateDto updateInfo)
        {
            var anime = await _repository.GetByIdAsync(animeId);
            anime.Status = updateInfo.Status;
            await _repository.UpdateAsync(anime);
            return anime;
        }
    }
}
