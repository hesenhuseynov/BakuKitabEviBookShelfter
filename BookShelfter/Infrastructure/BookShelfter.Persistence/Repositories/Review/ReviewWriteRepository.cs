using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.Review;
using BookShelfter.Domain.Entities;
using BookShelfter.Persistence.Contexts;

namespace BookShelfter.Persistence.Repositories.Review
{
    public  class ReviewWriteRepository:WriteRepository<Reviews>,IReviewWriteRepository
    {
        public ReviewWriteRepository(BookShelfterDbContext context) : base(context)
        {
        }
    }
}
