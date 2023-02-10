using Microsoft.AspNetCore.Http.HttpResults;
using QuizGame.Shared.Models;

namespace QuizGame.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizGameController : ControllerBase
{
    private readonly ILogger<QuizGameController> _logger;
    private readonly QuizGameDbContext _dbContext;

    public QuizGameController(ILogger<QuizGameController> logger, QuizGameDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IResult> GetQuizzes()
    {
        var result = await _dbContext.Quizzes.ToListAsync();
        return result.Any() ? Results.Ok(result) : Results.NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetQuizById([FromRoute] Guid id)
    {
        var result = await _dbContext.Quizzes.FindAsync(id);
        return result is not null ? Results.Ok(result) : Results.NoContent();
    }

    [HttpPost]
    public async Task<IResult> Post([FromBody] QuizViewModel quizViewModel)
    {
        var quiz = new Quiz
        {
            Id = Guid.NewGuid(),
            Title = quizViewModel.Title,
            Description = quizViewModel.Description,
            ImageUrl = quizViewModel.ImageUrl,
        };
        await _dbContext.Quizzes.AddAsync(quiz);
        return Results.Created($"/api/QuizGame/{quiz.Id}", quiz);
    }

    [HttpPut("{id}")]
    public async Task<IResult> Put(Guid id, [FromBody] QuizViewModel quizViewModel)
    {
        var quiz = await _dbContext.Quizzes.FindAsync(id);
        if (quiz is null) return Results.NoContent();
        quiz.Title = quizViewModel.Title;
        quiz.Description = quizViewModel.Description;
        quiz.ImageUrl = quizViewModel.ImageUrl;
        await _dbContext.SaveChangesAsync();
        return Results.Ok(quiz);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> Delete(Guid id)
    {
        var quiz = await _dbContext.Quizzes.FindAsync(id);
        if (quiz is null) return Results.NotFound();
        _dbContext.Quizzes.Remove(quiz);
        await _dbContext.SaveChangesAsync();
        return Results.Ok();
    }

    [HttpPut("{id}/AddFirstQuestion")]
    public async Task<IResult> AddQuestion(Guid id, [FromBody] Question question)
    {
        var quiz = await _dbContext.Quizzes.FindAsync(id);
        if (quiz is null) return Results.NoContent();

        if (quiz.FirstQuestion is null || quiz.LastQuestion is null)
        {
            quiz.FirstQuestion = question;
            quiz.LastQuestion = question;
        }
        else
        {
            quiz.FirstQuestion.Next ??= question;
            quiz.LastQuestion.Next = question;
            question.Previous = quiz.LastQuestion;
            quiz.LastQuestion = question;
        }

        await _dbContext.SaveChangesAsync();
        return Results.Ok(quiz);
    }
}