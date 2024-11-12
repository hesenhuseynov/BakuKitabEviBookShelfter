using BookShelfter.Domain.Entities.Common;
using BookShelfter.Domain.Entities.Identity;

namespace BookShelfter.Domain.Entities;

public class Basket:BaseEntity
{
    public string  UserId { get; set; }
    public AppUser User { get; set; }

    public int? OrderId { get; set; }
    public Order order { get; set; }
    
    public ICollection<BasketItem> BasketItems { get; set; }


    

    
}