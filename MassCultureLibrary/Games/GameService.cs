namespace MassCultureLibrary.Games
{
    public class GameService : IGameService
    {
        IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<Game> AddGameAsync(Game game)
        {
            //if (game == null)
            //    throw new ArgumentException("Game is null");
            return await _repository.AddAsync(game);
        }

        public async Task DeleteGameAsync(Guid gameId)
        {
            var game = await _repository.GetByIdAsync(gameId);
            if (game == null)
                throw new KeyNotFoundException($"Игра с Id: {gameId} не найдена. ");

            await _repository.DeleteAsync(gameId);
        }

        public async Task<Game?> GetGameByIdAsync(Guid gameId)
        {
            return await _repository.GetByIdAsync(gameId);
        }

        public Task<IEnumerable<Game>> GetGamesByPlatformAsync(string platform)
        {
            throw new NotImplementedException();
        }

        public async Task<Game> UpdateGameAsync(Guid gameId, GameUpdateDto updateInfo)
        {
            var game = await _repository.GetByIdAsync(gameId);

            if (game == null)
                throw new GameNotFoundException(gameId);

            game.Platform = updateInfo.Platform;

            await _repository.UpdateAsync(game);
            return game;
        }

        public Task<IEnumerable<Game>> GetGamesByGenreAsync(string platform)
        {
            throw new NotImplementedException();
        }
    }
}
