using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace MassCultureLibrary.Games
{
    public class JsonGameStorage :IGameRepository
    {
        public string _filename = "game.json";
        public List<Game> _games;

        public JsonSerializerOptions _options = new JsonSerializerOptions() { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), WriteIndented = true  };
        public JsonGameStorage()
        {
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            _games = JsonSerializer.Deserialize<List<Game>>(file, _options) ?? new List<Game>();
        }

        public async Task<Game> AddAsync(Game game)
        {
            _games.Add(game);
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<List<Game>>(file, _games, _options);
            return game;
        }

        public async Task DeleteAsync(Guid id)
        {
            _games.RemoveAll(x => x.Id == id);
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<List<Game>>(file, _games, _options);
        }

        public async Task<Game?> GetByIdAsync(Guid id)
        {
            var result = _games.Find(x => x.Id == id);
            return result;
        }

        public async Task UpdateAsync(Game game)
        {
            var result = _games.Find(x => x.Id == game.Id);
            result.Title = game.Title;
            result.Platform = game.Platform;
            result.Genre = game.Genre;
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<List<Game>>(file, _games, _options);
        }
    }
}
