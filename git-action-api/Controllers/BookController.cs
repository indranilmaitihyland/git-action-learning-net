using Microsoft.AspNetCore.Mvc;
using git_action_api.Repository;
using git_action_api.Models;

namespace git_action_api.Controllers
{
    
   
    // BookAPI/Controllers/BooksController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            var createdBook = await _bookRepository.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.Id) return BadRequest();

            var updatedBook = await _bookRepository.UpdateBookAsync(book);
            if (updatedBook == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookRepository.DeleteBookAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
