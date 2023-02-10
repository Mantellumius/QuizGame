namespace QuizGame.Shared.Models;

public class Question : Entity<Guid>
{
    public string Text { get; set; } = null!;
    public Question? Previous { get; set; }
    public Question? Next { get; set; }
}