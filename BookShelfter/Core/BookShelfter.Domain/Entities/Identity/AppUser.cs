using System.Collections;
using BookShelfter.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace BookShelfter.Domain.Entities.Identity;

public class AppUser:IdentityUser<string>
{
    public string NameSurName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
    public ICollection<Basket> Baskets { get; set; }



    public Customer Customer { get; set; }



    
}