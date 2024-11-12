using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Book;

namespace BookShelfter.Application.Features.Queries.Book.GetRelatedBooksByCategory
{
    public  class GetRelatedBooksByCategoryQueryResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        
        public IEnumerable<BookDto> BookDtos { get; set; }
         
    }
}
