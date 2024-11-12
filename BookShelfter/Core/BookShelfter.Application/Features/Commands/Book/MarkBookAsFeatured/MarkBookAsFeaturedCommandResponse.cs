using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.MarkBookAsFeatured
{
    public  class MarkBookAsFeaturedCommandResponse
    {
        public bool  IsSuccess { get; set; }
        public string Message  { get; set; }
    }
}
