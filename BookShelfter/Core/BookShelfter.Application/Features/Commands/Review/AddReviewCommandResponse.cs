using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Review
{
    public  class AddReviewCommandResponse
    {
        public int ReviewId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

