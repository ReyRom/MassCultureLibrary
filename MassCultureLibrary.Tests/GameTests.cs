using FluentAssertions;
using MassCultureLibrary.Games;
using Moq;

namespace MassCultureLibrary.Tests
{
    public class GameTests
    {
        private readonly IGameService _gameService;
        private readonly Game _game;
        public GameTests()
        {
            var gameRepository = new Mock<IGameRepository>();
            var gameService = new GameService(gameRepository.Object);
            _gameService = gameService;
            _game = new Game { Id = Guid.NewGuid(), Title = "The Witcher 3", Genre = "RPG", Platform = "PC" };
        }

        [Fact]
        public async Task AddGame_ShouldAddGameSuccessfully()
        {
            var game = _game;

            var result = await _gameService.AddGameAsync(game);

            result.Should().NotBeNull();
            result.Title.Should().Be("The Witcher 3");
        }

        [Fact]
        public async Task GetGamesByPlatform_ShouldReturnGamesForPlatform()
        {
            var platform = "PC";

            var games = await _gameService.GetGamesByPlatformAsync(platform);

            games.Should().NotBeEmpty();
            games.All(g => g.Platform == platform).Should().BeTrue();
        }

        [Fact]
        public async Task UpdateGame_ShouldChangeGamePlatform()
        {
            var gameId = _game.Id;
            var updateInfo = new GameUpdateDto { Platform = "PlayStation" };

            var updatedGame = await _gameService.UpdateGameAsync(gameId, updateInfo);

            updatedGame.Should().NotBeNull();
            updatedGame.Platform.Should().Be("PlayStation");
        }

        [Fact]
        public async Task DeleteGame_ShouldRemoveGame()
        {
            var gameId = _game.Id;

            var gameRepository = new Mock<IGameRepository>();
            gameRepository.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync(_game);
            gameRepository.Setup(repo => repo.DeleteAsync(gameId)).Returns(Task.CompletedTask);

            var gameService = new GameService(gameRepository.Object);

            await gameService.DeleteGameAsync(gameId);

            gameRepository.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync((Game)null);

            var game = await gameService.GetGameByIdAsync(gameId);
            game.Should().BeNull();
        }
    }

}
