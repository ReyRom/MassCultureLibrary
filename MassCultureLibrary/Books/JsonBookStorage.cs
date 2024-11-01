using MassCultureLibrary.Animes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace MassCultureLibrary.Books
{
    public class JsonBookStorage:IBookRepository
    {
        public string _filename = "book.json";
        public List<Book> _books;
        public JsonSerializerOptions _options = new JsonSerializerOptions() { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), WriteIndented = true };
        public JsonBookStorage()
        {
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            _books = JsonSerializer.Deserialize<List<Book>>(file, _options) ?? new List<Book>();
        }

        public async Task<Book> AddAsync(Book book)
        {
            _books.Add(book);
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<List<Book>>(file, _books, _options);
            return book;
        }

        public async Task DeleteAsync(Guid id)
        {
            _books.RemoveAll(x => x.Id == id);
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<List<Book>>(file, _books, _options);
        }

        public async Task<IEnumerable<Book>> GetByAuthorAsync(string author)
        {
            var result = _books.Where(x => x.Author == author).ToList();
            return result;
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            var result = _books.Find(x => x.Id == id);
            return result;
        }

        public async Task UpdateAsync(Book book)
        {
            var result = _books.Find(x => x.Id == book.Id);
            result.Title = book.Title;
            result.Author = book.Author;
            result.Genre = book.Genre;
            using FileStream file = new FileStream(_filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<List<Book>>(file, _books, _options);
        }
    }
}
