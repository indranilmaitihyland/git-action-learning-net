using git_action_api.Models;

namespace git_action_api.Repository
{

    // BookAPI/Repositories/BookRepository.cs
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books;

        public BookRepository()
        {
            _books = new List<Book>
            {
                 new Book
            {
                Id = 1,
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Year = 1925,
                ISBN = "978-0743273565"
            },
            new Book
            {
                Id = 2,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Year = 1960,
                ISBN = "978-0446310789"
            },
            new Book
            {
                Id = 3,
                Title = "1984",
                Author = "George Orwell",
                Year = 1949,
                ISBN = "978-0451524935"
            },
            new Book
            {
                Id = 4,
                Title = "The Catcher in the Rye",
                Author = "J.D. Salinger",
                Year = 1951,
                ISBN = "978-0316769488"
            },
            new Book
            {
                Id = 5,
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                Year = 1813,
                ISBN = "978-0141439518"
            }
            };
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await Task.FromResult(_books);
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await Task.FromResult(_books.FirstOrDefault(b => b.Id == id));
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            book.Id = _books.Count + 1;
            _books.Add(book);
            return await Task.FromResult(book);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook == null) return null;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;
            existingBook.ISBN = book.ISBN;

            return await Task.FromResult(existingBook);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null) return await Task.FromResult(false);

            _books.Remove(book);
            return await Task.FromResult(true);
        }
    }

}
