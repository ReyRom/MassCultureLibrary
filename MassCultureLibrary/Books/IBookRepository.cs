namespace MassCultureLibrary.Books
{
    public interface IBookRepository
    {
        Task<Book> AddAsync(Book book);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Guid id);
    }

}
