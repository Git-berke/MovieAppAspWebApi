using MovieReview.Api.Entities;
namespace MovieReview.Api.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetByIdsAsync(List<int> ids);
    }
}