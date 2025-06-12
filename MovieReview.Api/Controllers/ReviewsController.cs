using Microsoft.AspNetCore.Mvc;
using MovieReview.Api.DTOs;
using MovieReview.Api.Entities;
using MovieReview.Api.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReview.Api.Controllers
{
    [Route("api/movies/{movieId}/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly Data.ApiDbContext _context; // SaveChanges için geçici olarak kullanıyoruz

        public ReviewsController(IReviewRepository reviewRepository, IMovieRepository movieRepository, Data.ApiDbContext context)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddReviewForMovie(int movieId, [FromBody] ReviewCreateDto reviewDto)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);
            if (movie == null)
            {
                return NotFound("Film bulunamadı.");
            }

            var review = new Review
            {
                ReviewerName = reviewDto.ReviewerName,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                ReviewDate = DateTime.UtcNow,
                MovieId = movieId
            };

            await _reviewRepository.AddAsync(review);

            // Yeni ortalama puanı hesapla
            // Mevcut review'ları ve yeni ekleneni birleştirip ortalama al
            var allReviewsForMovie = movie.Reviews.Append(review);
            movie.AverageRating = allReviewsForMovie.Average(r => r.Rating);

            // Değişiklikleri tek bir işlemde kaydet
            await _context.SaveChangesAsync();

            // Not: Dönen cevabı da bir DTO'ya çevirmek en iyi pratiktir.
            return Ok(review);

        }
    }
}