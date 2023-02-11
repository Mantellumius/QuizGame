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
}