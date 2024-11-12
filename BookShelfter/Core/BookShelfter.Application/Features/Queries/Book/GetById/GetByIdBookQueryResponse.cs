using BookShelfter.Application.Common;
using BookShelfter.Application.DTOs;
using BookShelfter.Application.DTOs.Book;

namespace BookShelfter.Application.Features.Queries.Book.GetById;

public class GetByIdBookQueryResponse
{
    //public string Name { get; set; }
    //public int Stock { get; set; }
    //public decimal Price { get; set; }

    //public string Description { get; set; }

     public BookDto Book { get; set; }

     public bool  Success { get; set; }

     public string Message  { get; set; }

    // public ICollection<ProductImageFileDto> BookImageFiles { get; set; }
     
    
}