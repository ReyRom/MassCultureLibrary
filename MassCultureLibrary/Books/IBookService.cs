
namespace MassCultureLibrary.Books
{
    public interface IBookService
    {
        Task<Book> AddBookAsync(Book book);
        Task DeleteBookAsync(Guid bookId);
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author);
        Task<Book?> GetBookByIdAsync(Guid bookId);
        Task<Book> UpdateBookAsync(Guid bookId, BookUpdateDto updateInfo);
    }
}