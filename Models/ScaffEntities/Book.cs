using System;
using System.Collections.Generic;

namespace MyBookApp.Models.ScaffEntities;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateTime? PublishedDate { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
