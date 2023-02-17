namespace QuizGame.Server.ViewModels;

public class QuizViewModel
{
    public Guid AuthorId { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = "";
    public string? ImageUrl { get; set; }
}