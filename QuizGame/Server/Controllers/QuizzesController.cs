using Microsoft.AspNetCore.Http.HttpResults;
using QuizGame.Shared.Models;

namespace QuizGame.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly ILogger<QuizzesController> _logger;
    private readonly QuizGameDbContext _dbContext;

    public QuizzesController(ILogger<QuizzesController> logger, QuizGameDbContext dbContext)
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
            Id = quizViewModel.Id,
            Title = quizViewModel.Title,
            Description = quizViewModel.Description,
            ImageUrl = quizViewModel.ImageUrl,
        };
        await _dbContext.Quizzes.AddAsync(quiz);
        await _dbContext.SaveChangesAsync();
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

    [HttpPut("{id}/AddQuestion")]
    public async Task<IResult> AddQuestion(Guid id, [FromBody] Question question)
    {
        var quiz = await _dbContext.Quizzes.FindAsync(id);
        if (quiz is null) return Results.NoContent();
        quiz.Questions.Add(question);
        _dbContext.Quizzes.Update(quiz);
        await _dbContext.SaveChangesAsync();
        return Results.Ok(quiz);
    }
}