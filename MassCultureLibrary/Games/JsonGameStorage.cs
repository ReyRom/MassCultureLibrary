using MassCultureLibrary.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MassCultureLibrary.Games
{
    public class JsonGameStorage :IGameRepository
    {
        public string _filename = "game.json";
        public List<Game> _games;

        public JsonGameStorage()
        {
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            _games = JsonSerializer.Deserialize<List<Game>>(file) ?? new List<Game>();
        }

        public async Task<Game> AddAsync(Game game)
        {
            _games.Add(game);
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<List<Game>>(file, _games);
            return game;
        }

        public async Task DeleteAsync(Guid id)
        {
            _games.RemoveAll(x => x.Id == id);
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<List<Game>>(file, _games);
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
            await JsonSerializer.SerializeAsync<List<Game>>(file, _games);
        }
    }
}
