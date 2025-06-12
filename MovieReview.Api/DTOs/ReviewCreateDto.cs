using System.ComponentModel.DataAnnotations;

namespace MovieReview.Api.DTOs
{
    public class ReviewCreateDto
    {
        [Required]
        public string ReviewerName { get; set; }
        [Range(1, 5)] // Puan 1 ile 5 arasında olmalı
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}