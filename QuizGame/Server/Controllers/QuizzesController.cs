namespace QuizGame.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly QuizGameDbContext _dbContext;

    public QuizzesController(QuizGameDbContext dbContext)
    {
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
            .FirstOrDefaultAsync(x => x.Id == id);
        return result is not null ? Results.Ok(result) : Results.NoContent();
    }

    [Authorize]
    [HttpPost]
    public async Task<IResult> Post([FromBody] QuizDto quizDto)
    {
        var quiz = new Quiz
        {
            AuthorId = quizDto.AuthorId,
            Id = quizDto.Id,
            Title = quizDto.Title,
            Description = quizDto.Description,
            ImageUrl = quizDto.ImageUrl
        };
        await _dbContext.Quizzes.AddAsync(quiz);
        await _dbContext.SaveChangesAsync();
        return Results.Created($"/api/QuizGame/{quiz.Id}", quiz);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IResult> Put(Guid id, [FromBody] QuizDto quizDto)
    {
        var quiz = await _dbContext.Quizzes.FindAsync(id);
        if (quiz is null) return Results.NoContent();
        quiz.Title = quizDto.Title;
        quiz.Description = quizDto.Description;
        quiz.ImageUrl = quizDto.ImageUrl;
        quiz.Views = quizDto.Views;
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

    [Authorize]
    [HttpPut("{id:guid}/AddView")]
    public async Task<IResult> AddView(Guid id)
    {
        var quiz = await _dbContext.Quizzes.FindAsync(id);
        if (quiz is null) return Results.NoContent();
        quiz.Views++;
        _dbContext.Quizzes.Update(quiz);
        await _dbContext.SaveChangesAsync();
        return Results.Ok(quiz);
    }
}