using FluentAssertions;
using MassCultureLibrary.Animes;
using Moq;

namespace MassCultureLibrary.Tests
{
    public class AnimeTests
    {
        private IAnimeService _animeService;
        private readonly IAnimeRepository _animeRepository;
        private readonly Mock<IAnimeRepository> _mockAnimeRepository;
        private readonly Anime _anime;
        public AnimeTests()
        {
            _animeRepository = new JsonAnimeStorage();
            _mockAnimeRepository = new Mock<IAnimeRepository>();
            _animeService = new AnimeService(_animeRepository);
            _anime = new Anime {Id = Guid.NewGuid(), Title = "Наруто", Genre = "Экшен", Status = "Завершено" };
        }

        [Fact]
        public async Task AddAnime_ShouldAddAnimeCorrectly()
        {
            var anime = _anime;
            var result = await _animeService.AddAnimeAsync(anime);

            result.Should().NotBeNull();
            result.Title.Should().Be("Наруто");
        }

        [Fact]
        public async Task GetAnimeByStatus_ShouldReturnAnimeWithCorrectStatus()
        {
            var status = "Онгоинг";

            var animes = await _animeService.GetAnimeByStatusAsync(status);

            animes.Should().NotBeEmpty();
            animes.All(a => a.Status == status).Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAnime_ShouldUpdateAnimeStatus()
        {
            var animeId = _anime.Id;
            var updateInfo = new AnimeUpdateDto { Status = "Онгоинг" };

            var updatedAnime = await _animeService.UpdateAnimeAsync(animeId, updateInfo);

            updatedAnime.Should().NotBeNull();
            updatedAnime.Status.Should().Be("Онгоинг");
        }

        [Fact]
        public async Task DeleteAnime_ShouldRemoveAnime()
        {
            var animeId = _anime.Id;

            await _animeService.DeleteAnimeAsync(animeId);

            var anime = await _animeService.GetAnimeByIdAsync(animeId);
        }

        [Fact]
        public async Task DeleteAnimeByName_ShouldRemoveAnime()
        {
            await _animeService.DeleteByNameAsync(_anime.Title);
        }
    }

}
