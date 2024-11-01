using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassCultureLibrary.Games
{
    public class GameService:IGameService
    {
        IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<Game> AddGameAsync(Game game)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteGameAsync(Guid gameId)
        {
            await _gameRepository.DeleteAsync(gameId);
        }

        public Task<Game?> GetGameByIdAsync(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetGamesByPlatformAsync(string platform)
        {
            throw new NotImplementedException();
        }

        public Task<Game> UpdateGameAsync(Guid gameId, GameUpdateDto updateInfo)
        {
            throw new NotImplementedException();
        }
    }
}
