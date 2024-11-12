using BookShelfter.Application.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.Book.GetBookDetails
{
    public  class GetBookDetailsQueryResponse
    {
        public BookDetailDto Book { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

    }
}
