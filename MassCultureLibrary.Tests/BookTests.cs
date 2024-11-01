using FluentAssertions;
using MassCultureLibrary.Books;
using Moq;

namespace MassCultureLibrary.Tests
{
    public class BookTests
    {
        private readonly IBookService _bookService;
        private readonly Book _book;
        public BookTests()
        {
            var bookRepository = new JsonBookStorage();
            var bookService = new BookService(bookRepository);
            _bookService = bookService;
            _book = new Book { Id = Guid.NewGuid(), Title = "1984", Author = "Джордж Оруэлл", Genre = "Антиутопия" };
            bookRepository.AddAsync(_book).Wait();
        }

        [Fact]
        public async Task AddBook_ShouldAddBookSuccessfully()
        {
            var book = _book;

            var result = await _bookService.AddBookAsync(book);

            result.Should().NotBeNull();
            result.Title.Should().Be("1984");
        }

        [Fact]
        public async Task GetBooksByAuthor_ShouldReturnBooksByGivenAuthor()
        {
            var author = "Джордж Оруэлл";

            var books = await _bookService.GetBooksByAuthorAsync(author);

            books.Should().NotBeEmpty();
            books.All(b => b.Author == author).Should().BeTrue();
        }

        [Fact]
        public async Task UpdateBook_ShouldChangeBookTitle()
        {
            var bookId = _book.Id;
            var updateInfo = new BookUpdateDto { Title = "Скотный двор" };

            var updatedBook = await _bookService.UpdateBookAsync(bookId, updateInfo);

            updatedBook.Should().NotBeNull();
            updatedBook.Title.Should().Be("Скотный двор");
        }

        [Fact]
        public async Task DeleteBook_ShouldRemoveBook()
        {
            var bookId = _book.Id;

            await _bookService.DeleteBookAsync(bookId);

            var book = await _bookService.GetBookByIdAsync(bookId);
            book.Should().BeNull();
        }

        [Fact]
        public async Task GetBookByIdAsync_ReturnsCorrectBook()
        {
            var bookId = _book.Id;

            var books = await _bookService.GetBookByIdAsync(bookId);

            books.Should().NotBeNull();
            books.Title.Should().Be("1984");
        }
        [Fact]
        public async Task AddBook_WhenBookAlreadyExist()
        {
            var existingId = _book.Id;
            var existingBook = new Book { Id = existingId, Title = "1984" };

            await Assert.ThrowsAsync<ArgumentException>(async()=>await _bookService.AddBookAsync(existingBook));
        }
    }

}
