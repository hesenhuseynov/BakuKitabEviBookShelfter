using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Book.CreateBookWithImage
{
    public  class CreateBookWithImageCommandRequest:IRequest<CreateBookWithImageCommandResponse>
    {
        public string BookName { get; set; }
        public decimal  Price { get; set; }
        public int Stock { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
