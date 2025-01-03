using git_action_api.Models;

namespace git_action_api.Repository
{
    // BookAPI/Interfaces/IBookRepository.cs
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
    }

}
