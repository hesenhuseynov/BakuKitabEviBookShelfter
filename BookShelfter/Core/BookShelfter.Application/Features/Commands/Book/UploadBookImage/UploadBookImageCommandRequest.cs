using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.UploadBookImage
{
    public  class UploadBookImageCommandRequest:IRequest<UploadBookImageCommandResponse>
    {
        public IFormFile ImageFile { get; set; }
        public int BookId { get; set; }

        public UploadBookImageCommandRequest(int bookId,IFormFile imagefile)
        {
            ImageFile = imagefile;
            BookId = bookId;

        }


    }
}
