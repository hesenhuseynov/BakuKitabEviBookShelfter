using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.UpdateBook
{
    public  class UpdateBookCommandResponse
    {
        public string  Message { get; set; }
        public bool Success { get; set; }

        public string BookName  { get; set; }
        public int BookId { get; set; }

    }
}
