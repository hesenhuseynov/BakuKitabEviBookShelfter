using BookShelfter.Application.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.Book.SearchBooksQuery
{
    public  class SearchBooksQueryResponse
    {
        public List<BookDto> Books { get; set; }
        public int TotalCount { get; set; } 

    }
}
