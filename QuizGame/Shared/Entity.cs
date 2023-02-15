namespace QuizGame.Shared;

public abstract class Entity<TId>
{
    public TId Id { get; set; } = default!;
}