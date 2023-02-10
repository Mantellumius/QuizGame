using Microsoft.EntityFrameworkCore;
using QuizGame.Shared;
using QuizGame.Shared.Models;

namespace QuizGame.Server.DataAccess;

public class QuizGameDbContext : DbContext
{
    public QuizGameDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Question>()
            .HasOne(n => n.Next)
            .WithOne(n => n.Previous);

        modelBuilder.Entity<Question>()
            .HasOne(n => n.Previous)
            .WithOne(n => n.Next);
    }
}