using MovieReview.Api.Entities;
namespace MovieReview.Api.Repositories
{
    public interface IReviewRepository
    {
        Task AddAsync(Review review);
    }
}