using FluentAssertions;
using MassCultureLibrary.Games;
using Moq;

namespace MassCultureLibrary.Tests
{
    public class GameTests
    {
        private readonly GameService _gameService;
        private readonly Game _game;
        private readonly Mock<IGameService> _mockGameService;
        private readonly Mock<IGameRepository> _mockGameRepository;

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
        public async Task AddGame_ShouldReturnExceptionWhenTitleIsNull() // тест на попытку добавить игру без названия
        {
            var game = new Game { Id = Guid.NewGuid(), Genre = "aaa" };

            Func<Task> action = async () => await _mockGameService.Object.AddGameAsync(game);
            await action.Should().ThrowAsync<ArgumentNullException>().WithMessage("Game is null");
        }

        [Fact]
        public async Task AddGame_ShouldReturnExceptionWhenGameIdExists() //тест на попытку добавить игру с существующим id
        {
            var game = new Game { Id = _game.Id, Genre = "RPG", Platform = "PC", Title = "aaaa" };
           _mockGameRepository.Setup(repo => repo.AddAsync(game)).ThrowsAsync(new InvalidOperationException("Game with this Id exists"));

            Func<Task> action = async() => await _gameService.AddGameAsync(game);

            await action.Should().ThrowAsync<InvalidOperationException>().WithMessage("Game with this Id exists");
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
