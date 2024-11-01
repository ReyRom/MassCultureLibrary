using FluentAssertions;
using MassCultureLibrary.Games;
using Moq;

namespace MassCultureLibrary.Tests
{
    public class GameTests
    {
        private readonly IGameService _gameService;
        private readonly Game _game;
        private readonly Mock<IGameService> _mockGameService;

        public GameTests()
        {
            var gameRepository = new Mock<IGameRepository>();
            var gameService = new GameService(gameRepository.Object);
            _mockGameService = new Mock<IGameService>();
            _gameService = gameService;
            _game = new Game { Id = Guid.NewGuid(), Title = "The Witcher 3", Genre = "RPG", Platform = "PC" };
        }

        [Fact]
        public async Task AddGame_ShouldAddGameSuccessfully()
        {
            var game = _game;
            _mockGameService.Setup(g => g.AddGameAsync(game)).ReturnsAsync(game);
            var result = await _mockGameService.Object.AddGameAsync(game);

            Assert.NotNull(result);
            _mockGameService.Verify(repo => repo.AddGameAsync(game), Times.Once);
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

            await _gameService.DeleteGameAsync(gameId);

            var game = await _gameService.GetGameByIdAsync(gameId);
            game.Should().BeNull();
        }
    }

}
