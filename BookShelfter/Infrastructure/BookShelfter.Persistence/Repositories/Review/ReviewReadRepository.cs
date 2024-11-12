using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.Review;
using BookShelfter.Domain.Entities;
using BookShelfter.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Persistence.Repositories.Review
{
    public class ReviewReadRepository : ReadRepository<Reviews>, IReviewReadRepository
    {

        public ReviewReadRepository(BookShelfterDbContext context) : base(context)
        {
        }

        public async Task<Reviews?> GetReviewByBookAndUserAsync(int bookId, string userId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.BookID == bookId && r.UserID == userId);
        }

        public async Task<List<Reviews>?> GetReviewsByBookIdAsync(int bookId)
        {
            return await _context.Reviews.Where(r => r.BookID == bookId).ToListAsync();

        }

        public async Task<double> GetAverageRatingForBookAsync(int bookId)
        {
            var ratings = await _context.Reviews
          .Where(r => r.BookID == bookId)
          .Select(r => r.Rating)
          .ToListAsync();

            if (ratings == null || !ratings.Any())
            {
                return 0.0;
            }

            return ratings.Average();
        }

        public async Task<int> GetTotalReviewsForBookAsync(int bookId)
        {
            return await _context.Reviews
                .CountAsync(r => r.BookID == bookId);

        }
    }
}
