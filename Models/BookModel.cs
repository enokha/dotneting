namespace MyBookApp.Models
{
    public enum BookGenre
    {
        Fantasy,
        Comedy,
        Horror,
        Romance,
        SciFi,
        Thriller,
        Crime,
        Fiction
    }

    public class BookModel
    {
        public int Id { get; set; }
        public string FullTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public BookGenre? Genre { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
