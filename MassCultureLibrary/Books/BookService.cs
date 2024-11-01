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
        IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            return await _repository.AddAsync(book);
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
