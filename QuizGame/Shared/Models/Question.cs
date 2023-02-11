namespace QuizGame.Shared.Models;

public class Question : Entity<Guid>
{
    public string Text { get; set; } = null!;
    public List<Answer> Answers { get; set; } = new();
}