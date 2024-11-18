
namespace MassCultureLibrary.Animes
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAllAsync();
        Task<Anime> AddAsync(Anime anime);
        Task<Anime?> GetByIdAsync(Guid id);
        Task UpdateAsync(Anime anime);
        Task DeleteAsync(Guid id);
        Task<string> DeleteByNameAsync(string name);
        Task<string> UpdateAnimeNameAsync(string name);
        Task AddAnimeForLosersAsync(Anime anime);
    }

}
