# MovieReview.Api - Film Değerlendirme API'si

Bu proje, ASP.NET Core 8 kullanılarak geliştirilmiş, filmleri, film türlerini ve kullanıcı değerlendirmelerini yöneten bir Web API'sidir. Proje, portfolyo amaçlı oluşturulmuş olup modern .NET geliştirme pratiklerini sergilemektedir.

## Temel Özellikler

- Film ekleme, listeleme ve detaylarını görme.
- Filmlere ait türleri (genre) ve yorumları (review) ilişkili olarak getirme.
- Filmlere puan ve yorum ekleme.
- Bir filme yeni bir yorum eklendiğinde, filmin ortalama puanının otomatik olarak güncellenmesi.

## Kullanılan Teknolojiler ve Prensipier

- **Backend:** ASP.NET Core 8.0 Web API
- **Veritabanı:** SQLite & Entity Framework Core 8
- **Mimari Desenler:** Repository Pattern, DTO (Data Transfer Objects)
- **İlişki Yönetimi:** EF Core Fluent API ile bire-çok ve çoktan-çoğa ilişkilerin yapılandırılması.

## API Uç Noktaları (Endpoints)

Projede bulunan ana API uç noktaları aşağıdadır.

### Movies

- `GET /api/movies`: Tüm filmleri, türleri ve yorumları ile birlikte listeler.
- `GET /api/movies/{id}`: Belirtilen ID'ye sahip filmin tüm detaylarını getirir.
- `POST /api/movies`: Yeni bir film ekler.
  - **Örnek Gövde (Request Body):**
    ```json
    {
      "title": "Geleceğe Dönüş",
      "releaseYear": 1985,
      "director": "Robert Zemeckis",
      "genreIds": [ 1, 3 ]
    }
    ```

### Reviews

- `POST /api/movies/{movieId}/reviews`: Belirtilen filme yeni bir yorum ve puan ekler.
  - **Örnek Gövde (Request Body):**
    ```json
    {
      "reviewerName": "Mert",
      "rating": 5,
      "comment": "Gelmiş geçmiş en iyi filmlerden biri!"
    }
    ```

## Kurulum ve Çalıştırma

1.  Projeyi klonlayın: `git clone https://github.com/SENIN_KULLANICI_ADIN/MovieReview.Api.git`
2.  Proje klasörüne gidin: `cd MovieReview.Api`
3.  Veritabanını oluşturun ve başlangıç verilerini ekeleyin: `dotnet ef database update`
4.  Projeyi çalıştırın: `dotnet run`
5.  Swagger arayüzüne `https://localhost:PORT/swagger` adresinden ulaşabilirsiniz.
