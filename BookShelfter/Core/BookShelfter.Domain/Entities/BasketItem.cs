using BookShelfter.Domain.Entities.Common;

namespace BookShelfter.Domain.Entities;

public class BasketItem:BaseEntity
{
    public int  BasketId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public Basket Basket { get; set; }
    public Book  Book { get; set; } 


}