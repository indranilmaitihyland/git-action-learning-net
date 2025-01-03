using git_action_api.Controllers;
using git_action_api.Models;
using git_action_api.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace git_action_api.Tests
{
    public class BooksControllerTests
    {
        private readonly Mock<IBookRepository> _mockRepo;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _mockRepo = new Mock<IBookRepository>();
            _controller = new BooksController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetBooks_ReturnsOkResult_WithListOfBooks()
        {
            // Arrange
            var expectedBooks = new List<Book>
        {
            new Book { Id = 1, Title = "Test Book 1", Author = "Author 1" },
            new Book { Id = 2, Title = "Test Book 2", Author = "Author 2" }
        };
            _mockRepo.Setup(repo => repo.GetAllBooksAsync())
                    .ReturnsAsync(expectedBooks);

            // Act
            var result = await _controller.GetBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBooks = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.Equal(2, returnedBooks.Count());
        }

        [Fact]
        public async Task GetBook_ReturnsOkResult_WhenBookExists()
        {
            // Arrange
            var expectedBook = new Book { Id = 1, Title = "Test Book", Author = "Test Author" };
            _mockRepo.Setup(repo => repo.GetBookByIdAsync(1))
                    .ReturnsAsync(expectedBook);

            // Act
            var result = await _controller.GetBook(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBook = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(expectedBook.Id, returnedBook.Id);
        }

        [Fact]
        public async Task GetBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetBookByIdAsync(1))
                    .ReturnsAsync((Book)null);

            // Act
            var result = await _controller.GetBook(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateBook_ReturnsCreatedAtAction_WithNewBook()
        {
            // Arrange
            var bookToCreate = new Book { Title = "New Book", Author = "New Author" };
            var createdBook = new Book { Id = 1, Title = "New Book", Author = "New Author" };

            _mockRepo.Setup(repo => repo.AddBookAsync(It.IsAny<Book>()))
                    .ReturnsAsync(createdBook);

            // Act
            var result = await _controller.CreateBook(bookToCreate);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedBook = Assert.IsType<Book>(createdAtActionResult.Value);
            Assert.Equal(createdBook.Id, returnedBook.Id);
        }

        [Fact]
        public async Task UpdateBook_ReturnsNoContent_WhenUpdateSuccessful()
        {
            // Arrange
            var bookToUpdate = new Book { Id = 1, Title = "Updated Book", Author = "Updated Author" };
            _mockRepo.Setup(repo => repo.UpdateBookAsync(It.IsAny<Book>()))
                    .ReturnsAsync(bookToUpdate);

            // Act
            var result = await _controller.UpdateBook(1, bookToUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNoContent_WhenDeleteSuccessful()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteBookAsync(1))
                    .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteBook(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
