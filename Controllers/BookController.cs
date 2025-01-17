using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookApp.Data;
using MyBookApp.Models;
using MyBookApp.DTO;

namespace MyBookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            var result = books.Select(book => new BookDTOOut
            {
                Id = book.Id,
                FullTitle = book.FullTitle,
                Description = book.Description,
                Author = book.Author,
                Genre = book.Genre,
                PublishedDate = book.PublishedDate?.ToString("yyyy-MM-dd")
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound($"Book with ID {id} not found.");

            var bookDTO = new BookDTOOut
            {
                Id = book.Id,
                FullTitle = book.FullTitle,
                Description = book.Description,
                Author = book.Author,
                Genre = book.Genre,
                PublishedDate = book.PublishedDate?.ToString("yyyy-MM-dd")
            };

            return Ok(bookDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDTOIn bookDTOIn)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newBook = new BookModel
            {
                FullTitle = bookDTOIn.FullTitle,
                Description = bookDTOIn.Description,
                Author = bookDTOIn.Author,
                Genre = bookDTOIn.Genre,
                PublishedDate = bookDTOIn.PublishedDate
            };

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTOIn bookDTOIn)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null) return NotFound($"Book with ID {id} not found.");

            existingBook.FullTitle = bookDTOIn.FullTitle;
            existingBook.Description = bookDTOIn.Description;
            existingBook.Author = bookDTOIn.Author;
            existingBook.Genre = bookDTOIn.Genre;
            existingBook.PublishedDate = bookDTOIn.PublishedDate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound($"Book with ID {id} not found.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
