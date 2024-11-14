
namespace MassCultureLibrary.Games
{
    public interface IGameRepository
    {
        Task<Game> AddAsync(Game game);
        Task<Game?> GetByIdAsync(Guid id);
        Task UpdateAsync(Game game);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Game>> GetAllAsync();
    }

}
