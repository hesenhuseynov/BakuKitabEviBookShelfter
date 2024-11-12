using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.CreateBookWithImage
{
    public class CreateBookWithImageCommandResponse
    {
        public int BookId { get; set; }
        public string ImageUrl { get; set; }
    }
}
