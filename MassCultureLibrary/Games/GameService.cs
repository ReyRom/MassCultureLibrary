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
            if (game == null)
                throw new ArgumentException("Game is null");
            return await _repository.AddAsync(game);
        }

        public Task DeleteGameAsync(Guid gameId)
        {
            throw new NotImplementedException();
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
