using BookShelfter.Domain.Entities;

namespace BookShelfter.Application.Repositories.Review;

public interface IReviewReadRepository:IReadRepository<Reviews>
{
    Task<Reviews?> GetReviewByBookAndUserAsync(int bookId, string userId);
    Task<List<Reviews>?> GetReviewsByBookIdAsync(int bookId);
    Task<double> GetAverageRatingForBookAsync(int bookId);
    Task<int> GetTotalReviewsForBookAsync(int bookId);

}