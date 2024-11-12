using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Book.RemoveBookImage
{
    public  class RemoveBookImageCommandRequest:IRequest<RemoveBookImageCommandResponse>
    {
        public string ImageUrl  { get; set; }


        public RemoveBookImageCommandRequest(string imageUrl)
        {
            ImageUrl = imageUrl;

        }
         
        
    }
}
