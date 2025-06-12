using Microsoft.AspNetCore.Mvc;
using MovieReview.Api.DTOs;
using MovieReview.Api.Entities;
using MovieReview.Api.Repositories;

namespace MovieReview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly Data.ApiDbContext _context; // SaveChanges için geçici olarak kullanıyoruz.

        public MoviesController(IMovieRepository movieRepository, IGenreRepository genreRepository, Data.ApiDbContext context)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDetailDto>>> GetMovies()
        {
            var movies = await _movieRepository.GetAllAsync();

            var movieDtos = movies.Select(m => new MovieDetailDto
            {
                Id = m.Id,
                Title = m.Title,
                ReleaseYear = m.ReleaseYear,
                Director = m.Director,
                AverageRating = m.AverageRating,
                Genres = m.Genres.Select(g => new GenreDto { Id = g.Id, Name = g.Name }).ToList(),
                Reviews = m.Reviews.Select(r => new ReviewDto { Id = r.Id, ReviewerName = r.ReviewerName, Rating = r.Rating, Comment = r.Comment, ReviewDate = r.ReviewDate }).ToList()
            });

            return Ok(movieDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDetailDto>> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieDetailDto
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                Director = movie.Director,
                AverageRating = movie.AverageRating,
                Genres = movie.Genres.Select(g => new GenreDto { Id = g.Id, Name = g.Name }).ToList(),
                Reviews = movie.Reviews.Select(r => new ReviewDto { Id = r.Id, ReviewerName = r.ReviewerName, Rating = r.Rating, Comment = r.Comment, ReviewDate = r.ReviewDate }).ToList()
            };

            return Ok(movieDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateDto movieDto)
        {
            var genres = await _genreRepository.GetByIdsAsync(movieDto.GenreIds);

            var movie = new Movie
            {
                Title = movieDto.Title,
                Director = movieDto.Director,
                ReleaseYear = movieDto.ReleaseYear,
                AverageRating = 0,
                Genres = genres.ToList()
            };

            await _movieRepository.AddAsync(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }
    }
}