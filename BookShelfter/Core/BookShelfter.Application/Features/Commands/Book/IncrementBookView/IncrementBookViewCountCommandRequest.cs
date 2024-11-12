using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Book.IncrementBookView
{
    public  class IncrementBookViewCountCommandRequest:IRequest<IncrementBookViewCountCommandResponse>
    {
        public int BookId { get; set; }

    }
}
