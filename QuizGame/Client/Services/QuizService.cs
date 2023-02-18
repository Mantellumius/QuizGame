using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using QuizGame.Client.Providers;
using QuizGame.Shared.Models;

namespace QuizGame.Client.Services;

public class QuizService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public QuizService(ILocalStorageService localStorageService, HttpClient httpClient,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        Init();
    }

    private async Task Init()
    {
        var jsonWebToken = await _localStorageService.GetItemAsync<string>("bearerToken");
        if (jsonWebToken is null) return;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jsonWebToken);
    }

    public async Task<IEnumerable<Quiz>?> GetQuizzes()
    {
        return await _httpClient.GetFromJsonAsync<Quiz[]>("api/Quizzes");
    }

    public async Task<HttpResponseMessage> UpdateOrCreateQuiz(Quiz quiz)
    {
        if (quiz.Id == Guid.Empty)
            return await CreateQuiz(quiz);
        return await UpdateQuiz(quiz);
    }

    private async Task<HttpResponseMessage> UpdateQuiz(Quiz quiz)
    {
        return await _httpClient.PutAsJsonAsync($"/api/Quizzes/{quiz.Id}", quiz);
    }

    private async Task<HttpResponseMessage> CreateQuiz(Quiz quiz)
    {
        quiz.Id = Guid.NewGuid();
        quiz.AuthorId = await _authenticationStateProvider.GetUserGuid();
        return await _httpClient.PostAsJsonAsync("/api/Quizzes", quiz);
    }

    public async Task<HttpResponseMessage> SetQuestions(Guid? quizId, List<Question> questions)
    {
        return await _httpClient.PutAsJsonAsync($"/api/Quizzes/{quizId}/SetQuestions", questions);
    }

    public async Task<Quiz?> GetQuiz(string id)
    {
        return await _httpClient.GetFromJsonAsync<Quiz>($"api/Quizzes/{id}");
    }
}