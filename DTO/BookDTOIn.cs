namespace MyBookApp.DTO
{
    public class BookDTOIn
    {
        public string? FullTitle { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int? Genre { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
