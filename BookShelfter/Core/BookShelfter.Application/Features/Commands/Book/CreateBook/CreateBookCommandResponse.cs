using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.CreateBook
{
    public  class CreateBookCommandResponse
    {
        public int BookId { get; set; }
        public string BookName  { get; set; }
        public bool  Success { get; set; }

        public string Message { get; set; }
    }
}
