namespace QuizGame.Server.ViewModels;

public class QuizDto
{
    public Guid AuthorId { get; set; } = default!;
    public Guid Id { get; set; } = default!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int Views { get; set; }
}