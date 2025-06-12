namespace MovieReview.Api.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } // Şimdilik basitçe bir isim alalım
        public int Rating { get; set; } // 1-5 arası puan
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        // Foreign Key: Bu değerlendirmenin hangi filme ait olduğu
        public int MovieId { get; set; }
        // Navigation Property: Her değerlendirme bir filme aittir.
        public Movie Movie { get; set; }
    }
}