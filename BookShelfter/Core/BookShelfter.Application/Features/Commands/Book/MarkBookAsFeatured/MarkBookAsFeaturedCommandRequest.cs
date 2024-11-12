using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Book.MarkBookAsFeatured
{
    public  class MarkBookAsFeaturedCommandRequest:IRequest<MarkBookAsFeaturedCommandResponse>
    {
        public int BookId  { get; set; }
    }
}
