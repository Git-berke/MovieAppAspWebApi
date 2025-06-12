namespace MovieReview.Api.DTOs
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public double AverageRating { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
        public ICollection<ReviewDto> Reviews { get; set; }
    }
}