using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using BookShelfter.Domain.Entities.Common;

namespace BookShelfter.Domain.Entities;

public class Category:BaseEntity
{
    [Required]
    public string CategoryName { get; set; }
    public ICollection<Book> Books { get; set; }
    
    
    
    
    
    
}