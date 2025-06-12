namespace MovieReview.Api.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public double AverageRating { get; set; } // Ortalama puanı tutacağımız alan

        // Navigation Property: Bir filmin birden çok değerlendirmesi olabilir.
        public ICollection<Review> Reviews { get; set; }

        // Navigation Property: Bir filmin birden çok türü olabilir.
        public ICollection<Genre> Genres { get; set; }
    }
}