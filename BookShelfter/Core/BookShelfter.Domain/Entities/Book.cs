using System.Collections;
using BookShelfter.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookShelfter.Domain.Entities;

public class Book:BaseEntity
{
    public string BookName { get; set; }
    public string AuthorName { get; set; }
    public int  Stock { get; set; }
    public decimal Price { get; set; }

    public string Description { get; set; }
    

    public int? LanguageId { get; set; }
    public Language Language { get; set; }
    
    public int? CategoryId { get; set; }
    public Category Category { get; set; }

    public bool IsDeleted  { get; set; }

    public bool IsFeatured { get; set; } // Önə çıxan productlar üçün 

    public  int ViewCount { get; set; }

    


    public ICollection<ProductImageFile> BookImagesFile { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }

    public ICollection<Reviews> Reviews { get; set; }
    
    
}