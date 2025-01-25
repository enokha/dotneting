using Microsoft.EntityFrameworkCore;
using MyBookApp.Data;
using MyBookApp.DTO;
using MyBookApp.Models;
using System.Globalization;

namespace MyBookApp.Services
{
    public class BookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookDTOOut>> GetAllBooksAsync()
        {
            var books = await _context.Books.ToListAsync();
            return books.Select(book => new BookDTOOut
            {
                Id = book.Id,
                FullTitle = book.FullTitle ?? string.Empty,
                Description = book.Description ?? string.Empty,
                Author = book.Author ?? string.Empty,
                Genre = book.Genre.HasValue ? (int)book.Genre.Value : null,
                PublishedDate = book.PublishedDate.HasValue 
                    ? book.PublishedDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) 
                    : null
            }).ToList();
        }

        public async Task<BookDTOOut?> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return null;

            return new BookDTOOut
            {
                Id = book.Id,
                FullTitle = book.FullTitle ?? string.Empty,
                Description = book.Description ?? string.Empty,
                Author = book.Author ?? string.Empty,
                Genre = book.Genre.HasValue ? (int)book.Genre.Value : null,
                PublishedDate = book.PublishedDate.HasValue 
                    ? book.PublishedDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) 
                    : null
            };
        }

        public async Task<BookDTOOut> CreateBookAsync(BookDTOIn bookDTOIn)
        {
            DateTime? parsedDate = ParseDate(bookDTOIn.PublishedDate?.ToString("yyyy-MM-dd"));

            var newBook = new BookModel
            {
                FullTitle = bookDTOIn.FullTitle ?? string.Empty,
                Description = bookDTOIn.Description ?? string.Empty,
                Author = bookDTOIn.Author ?? string.Empty,
                Genre = bookDTOIn.Genre.HasValue ? (BookGenre?)bookDTOIn.Genre : null,
                PublishedDate = parsedDate
            };

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            return new BookDTOOut
            {
                Id = newBook.Id,
                FullTitle = newBook.FullTitle,
                Description = newBook.Description,
                Author = newBook.Author,
                Genre = newBook.Genre.HasValue ? (int)newBook.Genre.Value : null,
                PublishedDate = newBook.PublishedDate.HasValue 
                    ? newBook.PublishedDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) 
                    : null
            };
        }

        public async Task<BookDTOOut?> UpdateBookAsync(int id, BookDTOIn bookDTOIn)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return null;

            DateTime? parsedDate = ParseDate(bookDTOIn.PublishedDate?.ToString("yyyy-MM-dd"));

            book.FullTitle = bookDTOIn.FullTitle ?? book.FullTitle;
            book.Description = bookDTOIn.Description ?? book.Description;
            book.Author = bookDTOIn.Author ?? book.Author;
            book.Genre = bookDTOIn.Genre.HasValue ? (BookGenre?)bookDTOIn.Genre : book.Genre;
            book.PublishedDate = parsedDate ?? book.PublishedDate;

            await _context.SaveChangesAsync();

            return new BookDTOOut
            {
                Id = book.Id,
                FullTitle = book.FullTitle,
                Description = book.Description,
                Author = book.Author,
                Genre = book.Genre.HasValue ? (int)book.Genre.Value : null,
                PublishedDate = book.PublishedDate.HasValue 
                    ? book.PublishedDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) 
                    : null
            };
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        private DateTime? ParseDate(string? dateString)
        {
            if (!string.IsNullOrEmpty(dateString))
            {
                if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    return parsedDate;
                }
            }
            return null;
        }
    }
}
