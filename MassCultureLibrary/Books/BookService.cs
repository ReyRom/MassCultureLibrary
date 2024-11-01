using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MassCultureLibrary.Books;
using Moq;

namespace MassCultureLibrary.Books
{
    public class BookService : IBookService
    {
        IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<Book> AddBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookAsync(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public Task<Book?> GetBookByIdAsync(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author)
        {
            return await _bookRepository.GetByAuthorAsync(author);
        }

        public Task<Book> UpdateBookAsync(Guid bookId, BookUpdateDto updateInfo)
        {
            throw new NotImplementedException();
        }
    }
}
