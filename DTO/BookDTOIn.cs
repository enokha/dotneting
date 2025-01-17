using MyBookApp.Models;

namespace MyBookApp.DTO
{
    public class BookDTOIn
    {
        public string FullTitle { get; set; } = string.Empty; // Fix
        public string Description { get; set; } = string.Empty; // Fix
        public string Author { get; set; } = string.Empty; // Fix
        public BookGenre? Genre { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
