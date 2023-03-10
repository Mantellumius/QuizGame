namespace QuizGame.Server.DataAccess;

public class QuizGameDbContext : DbContext
{
    public QuizGameDbContext(DbContextOptions<QuizGameDbContext> options) : base(options)
    {
    }

    public DbSet<Quiz> Quizzes { get; set; } = null!;
}