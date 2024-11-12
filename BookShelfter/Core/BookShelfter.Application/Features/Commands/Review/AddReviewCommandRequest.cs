using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Review;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Review
{
    public  class AddReviewCommandRequest:IRequest<AddReviewCommandResponse>
    {
        public int BookId { get; set; }
        public string UserId { get; set; }

        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }
}
