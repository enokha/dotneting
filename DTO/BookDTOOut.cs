using MyBookApp.Models;

namespace MyBookApp.DTO
{
    public class BookDTOOut
    {
        public int Id { get; set; }
        public string FullTitle { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public BookGenre? Genre { get; set; }
        public string PublishedDate { get; set; }
    }
}
