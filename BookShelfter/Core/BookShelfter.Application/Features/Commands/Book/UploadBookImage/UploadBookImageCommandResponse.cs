using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.UploadBookImage
{
    public  class UploadBookImageCommandResponse
    {
        public bool Success { get; set; }
        public string Message {  get; set; }

        public string ImageUrl { get; set; }

      
    }
}
