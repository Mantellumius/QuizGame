namespace QuizGame.Shared.Models;

public class Answer : Entity<Guid>
{
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
}