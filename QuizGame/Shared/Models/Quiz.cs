namespace QuizGame.Shared.Models;

public class Quiz : Entity<Guid>
{
    public string? ImageUrl { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = "";
    public Question? FirstQuestion { get; set; }
    public Question? LastQuestion { get; set; }
}