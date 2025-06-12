using Microsoft.EntityFrameworkCore;
using MovieReview.Api.Entities;



namespace MovieReview.Api.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -- Veri Tohumlama (Data Seeding) --

            // 1. Türleri (Genres) Ekle
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Bilim Kurgu" },
                new Genre { Id = 2, Name = "Aksiyon" },
                new Genre { Id = 3, Name = "Macera" },
                new Genre { Id = 4, Name = "Dram" }
            );

            // 2. Filmleri (Movies) Ekle
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Inception", ReleaseYear = 2010, Director = "Christopher Nolan", AverageRating = 0 },
                new Movie { Id = 2, Title = "The Matrix", ReleaseYear = 1999, Director = "Wachowskis", AverageRating = 0 }
            );

            // 3. Değerlendirmeleri (Reviews) Ekle
            // 3. Değerlendirmeleri (Reviews) Ekle
            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, MovieId = 1, ReviewerName = "Ahmet", Rating = 5, Comment = "Harika bir film!", ReviewDate = new DateTime(2024, 5, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Review { Id = 2, MovieId = 1, ReviewerName = "Zeynep", Rating = 4, Comment = "Biraz kafa karıştırıcı ama etkileyici.", ReviewDate = new DateTime(2024, 5, 2, 0, 0, 0, DateTimeKind.Utc) },
                new Review { Id = 3, MovieId = 2, ReviewerName = "Mehmet", Rating = 5, Comment = "Sinema tarihinde bir devrim.", ReviewDate = new DateTime(2024, 5, 3, 0, 0, 0, DateTimeKind.Utc) }
            );

            // -- ÇOKTAN ÇOĞA İLİŞKİYİ YAPILANDIRMA (DÜZELTİLMİŞ HALİ) --
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieGenres", // Ara tablonun adı
                    j => j.HasOne<Genre>().WithMany().HasForeignKey("GenresId"),
                    j => j.HasOne<Movie>().WithMany().HasForeignKey("MoviesId"),
                    j =>
                    {
                        // HATAYI ÇÖZEN KISIM: Bileşik anahtar tanımı
                        j.HasKey("MoviesId", "GenresId");

                        // Ara tabloya başlangıç verilerini ekle
                        j.HasData(
                            new { MoviesId = 1, GenresId = 1 }, // Inception -> Bilim Kurgu
                            new { MoviesId = 1, GenresId = 2 }, // Inception -> Aksiyon
                            new { MoviesId = 1, GenresId = 3 }, // Inception -> Macera
                            new { MoviesId = 2, GenresId = 1 }, // The Matrix -> Bilim Kurgu
                            new { MoviesId = 2, GenresId = 2 }  // The Matrix -> Aksiyon
                        );
                    });
        }
    }

}