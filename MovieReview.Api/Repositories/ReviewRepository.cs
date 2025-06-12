using MovieReview.Api.Data;
using MovieReview.Api.Entities;
namespace MovieReview.Api.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApiDbContext _context;
        public ReviewRepository(ApiDbContext context) { _context = context; }

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
        }
    }
}