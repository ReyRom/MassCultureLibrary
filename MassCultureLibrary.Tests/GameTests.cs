using FluentAssertions;
using MassCultureLibrary.Games;
using Moq;

namespace MassCultureLibrary.Tests
{
    public class GameTests
    {
        private IGameService _gameService;
        private readonly IGameRepository _gameRepository;
        private readonly Mock<IGameRepository> _mockGameRepository;
        private readonly Game _game;

        public GameTests()
        {
            _gameRepository = new JsonGameStorage();
            _mockGameRepository = new Mock<IGameRepository>();
            _gameService = new GameService(_gameRepository);
            _game = new Game { Id = Guid.NewGuid(), Title = "The Witcher 3", Genre = "RPG", Platform = "PC" };
        }

        [Fact]
        public async Task AddGame_ShouldAddGameSuccessfully()
        {
            var game = _game;
            var result = await _gameService.AddGameAsync(game);

            Assert.NotNull(result);
            Assert.Equal(game, result);
        }

        [Fact]
        public async Task AddGame_ShouldReturnExceptionWhenTitleIsNull() // тест на попытку добавить игру без названия
        {
            var game = new Game { Id = Guid.NewGuid(), Genre = "aaa" };

            Func<Task> action = async () => await _gameService.AddGameAsync(game);
            await action.Should().ThrowAsync<ArgumentNullException>().WithMessage("Game is null");
        }

        [Fact]
        public async Task AddGame_ShouldReturnExceptionWhenGameIdExists() //тест на попытку добавить игру с существующим id
        {
            var game = new Game { Id = _game.Id, Genre = "RPG", Platform = "PC", Title = "aaaa" };

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
