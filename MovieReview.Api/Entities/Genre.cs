using MovieReview.Api.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation Property: Bir türün birden çok filmi olabilir.
    public ICollection<Movie> Movies { get; set; }
}