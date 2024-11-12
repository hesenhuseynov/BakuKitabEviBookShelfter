using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.DTOs.Book
{
    public  class CreateBookDto
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int PublisherId { get; set; }
        public int CategoryId { get; set; }


    }
}
