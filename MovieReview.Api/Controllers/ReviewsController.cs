using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReview.Api.Data;
using MovieReview.Api.Entities;

// Bu route yapısı, değerlendirmelerin filmlere bağlı olduğunu net bir şekilde ifade eder.
[Route("api/movies/{movieId}/reviews")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly ApiDbContext _context;

    public ReviewsController(ApiDbContext context)
    {
        _context = context;
    }

    // POST: api/movies/1/reviews
    [HttpPost]
    public async Task<IActionResult> AddReviewForMovie(int movieId, Review review)
    {
        // 1. Değerlendirmenin ekleneceği filmi bul.
        var movie = await _context.Movies.Include(m => m.Reviews).FirstOrDefaultAsync(m => m.Id == movieId);
        if (movie == null)
        {
            return NotFound("Film bulunamadı.");
        }

        // 2. Gelen review nesnesini filme bağla ve tarih ata.
        review.MovieId = movieId;
        review.ReviewDate = DateTime.UtcNow;

        // 3. Yeni değerlendirmeyi context'e ekle.
        _context.Reviews.Add(review);

        // 4. Filmin yeni ortalama puanını HESAPLA!
        // Filmin tüm değerlendirmelerini alıp yeni ekleneni de dahil ederek ortalama al.
        movie.AverageRating = movie.Reviews.Append(review).Average(r => r.Rating);

        // 5. Tüm değişiklikleri (yeni review ve güncellenmiş puan) veritabanına kaydet.
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMovieById", "Movies", new { id = movieId }, review);
    }
}