using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReview.Api.Data;
using MovieReview.Api.Entities;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly ApiDbContext _context;

    public GenresController(ApiDbContext context)
    {
        _context = context;
    }

    // GET: api/genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
    {
        // Tüm türleri veritabanından çek ve liste olarak döndür.
        var genres = await _context.Genres.ToListAsync();
        return Ok(genres);
    }
}