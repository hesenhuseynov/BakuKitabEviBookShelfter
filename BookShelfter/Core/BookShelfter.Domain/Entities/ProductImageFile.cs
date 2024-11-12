using BookShelfter.Domain.Entities.Common;

namespace BookShelfter.Domain.Entities;

public class ProductImageFile:File
{
    public string    ImageUrl { get; set; }
    public bool Showcase { get; set; } 
    public int BookId { get; set; }
    public Book Book { get; set; }

}