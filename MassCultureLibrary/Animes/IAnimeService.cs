
namespace MassCultureLibrary.Animes
{
    public interface IAnimeService
    {
        Task<IEnumerable<Anime>> GetAnimeAsync();
        Task<Anime> AddAnimeAsync(Anime anime);
        Task DeleteAnimeByIdAsync(Guid animeId);
        Task DeleteAnimeAsync(Anime anime);
        Task<Anime?> GetAnimeByIdAsync(Guid animeId);
        Task<Anime?> GetAnimeByTitleAsync(string title);
        Task<IEnumerable<Anime>> GetAnimeByStatusAsync(string status);
        Task<IEnumerable<Anime>> GetAnimeByGenreAsync(string genre);
        Task<Anime> UpdateAnimeAsync(Guid animeId, AnimeUpdateDto updateInfo);
    }
}