using Microsoft.EntityFrameworkCore;
using MovieReview.Api.Data;
using MovieReview.Api.Entities;

namespace MovieReview.Api.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApiDbContext _context;

        public MovieRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            // İlişkili verileri de getirmeyi unutmuyoruz!
            return await _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Reviews)
                .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
        }
    }
}