using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShelfter.Domain.Entities;

namespace BookShelfter.Application.Features.Queries.Review
{
    public class GetReviewsByBookIdQueryResponse
    {
        public string  Message { get; set; }
        public bool  Success { get; set; }
        public List<Reviews> Reviews { get; set; }

    }
}