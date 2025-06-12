using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReview.Api.Data;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly ApiDbContext _context;

    public MoviesController(ApiDbContext context)
    {
        _context = context;
    }

    // GET: api/movies
    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        // İlişkili verileri de getirmek için .Include() kullanıyoruz.
        var movies = await _context.Movies
            .Include(m => m.Genres) // Her filmin Türler (Genres) bilgisini de sorguya dahil et.
            .Include(m => m.Reviews) // Her filmin Değerlendirme (Reviews) bilgisini de dahil et.
            .ToListAsync();

        return Ok(movies);
    }

    // GET: api/movies/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        var movie = await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Reviews)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound(); // Film bulunamazsa 404 hatası döndür.
        }

        return Ok(movie);
    }
}