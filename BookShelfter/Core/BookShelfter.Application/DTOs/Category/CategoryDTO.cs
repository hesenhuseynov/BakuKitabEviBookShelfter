using BookShelfter.Application.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.DTOs.Category
{
    public  class CategoryDto
    {
        public string CategoryName { get; set; }
        public ICollection<BookDto> Books { get; set; }
    }
}
