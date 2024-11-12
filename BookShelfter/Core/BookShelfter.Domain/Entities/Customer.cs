using BookShelfter.Domain.Entities.Common;
using BookShelfter.Domain.Entities.Identity;

namespace BookShelfter.Domain.Entities;

public class Customer:BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }
    public string? PhoneNumber { get; set; }

    public string Address { get; set; }

    public ICollection<Order> Orders { get; set; }

    public string AppUserId { get; set; }

    public AppUser AppUser { get; set; }

}