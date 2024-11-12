using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Book.UpdateBook
{
    public  class UpdateBookCommandRequest:IRequest<UpdateBookCommandResponse>
    {
        public string BookName { get; set; }

        public  int BookId { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public int LanguageId { get; set; }
    }
}
