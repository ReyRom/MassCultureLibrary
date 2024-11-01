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

            var gameRepository = new Mock<IGameRepository>();
            gameRepository.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync(_game);
            gameRepository.Setup(repo => repo.DeleteAsync(gameId)).Returns(Task.CompletedTask);

            var gameService = new GameService(gameRepository.Object);

            await gameService.DeleteGameAsync(gameId);

            gameRepository.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync((Game)null);

            var game = await gameService.GetGameByIdAsync(gameId);
            game.Should().BeNull();
        }

        [Fact]
        public async Task GetGamesByGenre_ShouldReturnGamesForGenre()
        {
            var genre = "RPG";

            var games = await _gameService.GetGamesByGenreAsync(genre);

            games.Should().NotBeEmpty();
            games.All(g => g.Genre == genre).Should().BeTrue();
        }

        [Fact]
        public async Task UpdateGame_WithNonExistentGameId_ShouldReturnNullOrThrow()
        {
            var nonExistentGameId = Guid.NewGuid();
            var updateInfo = new GameUpdateDto { Platform = "Xbox" };

            Func<Task> action = async () => await _gameService.UpdateGameAsync(nonExistentGameId, updateInfo);

            await action.Should().ThrowAsync<GameNotFoundException>();
        }
    }

}
