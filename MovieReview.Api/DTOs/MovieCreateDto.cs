using System.ComponentModel.DataAnnotations;

namespace MovieReview.Api.DTOs
{
    public class MovieCreateDto
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        [Required]
        public string Director { get; set; }
        public List<int> GenreIds { get; set; } // Filmin hangi türlere ait olduğunu ID listesi olarak alacağız
    }
}