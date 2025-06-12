using Microsoft.EntityFrameworkCore;
using MovieReview.Api.Data;
using MovieReview.Api.Entities;
namespace MovieReview.Api.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApiDbContext _context;
        public GenreRepository(ApiDbContext context) { _context = context; }

        public async Task<IEnumerable<Genre>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Genres.Where(g => ids.Contains(g.Id)).ToListAsync();
        }
    }
}