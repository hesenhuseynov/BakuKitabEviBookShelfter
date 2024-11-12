using BookShelfter.Application.DTOs.Book;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.Book.GetFeaturedBooks
{
    public  class GetFeaturedBooksQueryResponse
    {
        public IEnumerable<BookDto> Books { get; set; }
        public int TotalBookCount { get; set; }
    }
}
