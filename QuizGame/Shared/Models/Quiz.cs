namespace QuizGame.Shared.Models;

public class Quiz : Entity<Guid>
{
    public Guid AuthorId { get; set; }
    public string? ImageUrl { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = "";
    public List<Question>? Questions { get; set; } = new();
}