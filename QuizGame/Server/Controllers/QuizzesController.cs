namespace QuizGame.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly QuizGameDbContext _dbContext;
    private readonly ILogger<QuizzesController> _logger;

    public QuizzesController(ILogger<QuizzesController> logger, QuizGameDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IResult> GetQuizzes()
    {
        var result = await _dbContext.Quizzes.Include(q => q.Questions).ToListAsync();
        return Results.Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IResult> GetQuizById([FromRoute] Guid id)
    {
        var result = await _dbContext.Quizzes.Include(q => q.Questions)!
            .ThenInclude(q => q.Answers)
            .FirstAsync(x => x.Id == id);
        return result is not null ? Results.Ok(result) : Results.NoContent();
    }

    [Authorize]
    [HttpPost]
    public async Task<IResult> Post([FromBody] QuizViewModel quizViewModel)
    {
        var quiz = new Quiz
        {
            AuthorId = quizViewModel.AuthorId,
            Id = quizViewModel.Id,
            Title = quizViewModel.Title,
            Description = quizViewModel.Description,
            ImageUrl = quizViewModel.ImageUrl
        };
        await _dbContext.Quizzes.AddAsync(quiz);
        await _dbContext.SaveChangesAsync();
        return Results.Created($"/api/QuizGame/{quiz.Id}", quiz);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
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

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete(Guid id)
    {
        var quiz = await _dbContext.Quizzes.FindAsync(id);
        if (quiz is null) return Results.NotFound();
        _dbContext.Quizzes.Remove(quiz);
        await _dbContext.SaveChangesAsync();
        return Results.Ok();
    }

    [Authorize]
    [HttpPut("{id:guid}/AddQuestion")]
    public async Task<IResult> AddQuestion(Guid id, [FromBody] Question question)
    {
        var quiz = await _dbContext.Quizzes.FindAsync(id);
        if (quiz is null) return Results.NoContent();
        quiz.Questions?.Add(question);
        _dbContext.Quizzes.Update(quiz);
        await _dbContext.SaveChangesAsync();
        return Results.Ok(quiz);
    }

    [Authorize]
    [HttpPut("{id:guid}/SetQuestions")]
    public async Task<IResult> SetQuestions(Guid id, [FromBody] List<Question> questions)
    {
        var quiz = await _dbContext.Quizzes.FindAsync(id);
        if (quiz is null) return Results.NoContent();
        quiz.Questions = questions;
        _dbContext.Quizzes.Update(quiz);
        await _dbContext.SaveChangesAsync();
        return Results.Ok(quiz);
    }
}