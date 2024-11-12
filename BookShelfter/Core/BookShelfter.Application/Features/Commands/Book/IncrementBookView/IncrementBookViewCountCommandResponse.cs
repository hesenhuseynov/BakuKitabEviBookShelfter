using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.IncrementBookView
{
    public  class IncrementBookViewCountCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
