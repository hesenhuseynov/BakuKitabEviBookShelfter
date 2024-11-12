using BookShelfter.Application.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.DTOs.Basket
{
    public  class BasketItemDto
    {
        public int BasketId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
         
        public decimal UnitPrice { get; set; }
        public BookDto Book { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
