using MassCultureLibrary.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace MassCultureLibrary.Movies
{
    internal class JsonMovieStorage : IMovieRepository
    {
        public string _filename = "movie.json";
        public List<Movie> _movies;
        public JsonSerializerOptions _options = new JsonSerializerOptions() { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), WriteIndented = true };
        public JsonMovieStorage()
        {
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            _movies = JsonSerializer.Deserialize<List<Movie>>(file,_options) ?? new List<Movie>();
        }
        public async Task<Movie> AddAsync(Movie movie)
        {
            _movies.Add(movie);
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<List<Movie>>(file, _movies, _options);
            return movie;
        }

        public async Task DeleteAsync(Guid id)
        {
            _movies.RemoveAll(x => x.Id == id);
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<List<Movie>>(file, _movies, _options);
        }

        public async Task<Movie?> GetByIdAsync(Guid id)
        {
            var result = _movies.Find(x => x.Id == id);
            return result;
        }

        public async Task UpdateAsync(Movie movie)
        {
            var result = _movies.Find(x => x.Id == movie.Id);
            result.Title = movie.Title;
            result.ReleaseYear = movie.ReleaseYear;
            result.Genre = movie.Genre;
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<List<Movie>>(file, _movies, _options);
        }
    }
}
