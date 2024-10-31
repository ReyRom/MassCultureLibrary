using MassCultureLibrary.Games;

namespace MassCultureLibrary.Games
{
    public interface IGameService
    {
        Task<Game> AddGameAsync(Game game);
        Task DeleteGameAsync(Guid gameId);
        Task<Game?> GetGameByIdAsync(Guid gameId);
        Task<IEnumerable<Game>> GetGamesByPlatformAsync(string platform);
        Task<Game> UpdateGameAsync(Guid gameId, GameUpdateDto updateInfo);
    }
}