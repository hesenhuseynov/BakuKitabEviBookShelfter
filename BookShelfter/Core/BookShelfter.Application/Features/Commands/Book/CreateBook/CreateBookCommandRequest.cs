using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.CreateBook
{
    public  class CreateBookCommandRequest:IRequest<CreateBookCommandResponse>
    {
        public string BookName { get; set; }
        public decimal  Price { get; set; }
        public int Stock { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public int LanguageId { get; set; }
    }
}
