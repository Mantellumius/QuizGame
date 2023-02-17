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
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jsonWebToken);
    }

    public async Task<HttpResponseMessage> CreateQuiz(Quiz quiz)
    {
        quiz.Id = Guid.NewGuid();
        quiz.AuthorId = await _authenticationStateProvider.GetUserGuid();
        return await _httpClient.PostAsJsonAsync("/api/Quizzes", quiz);
    }

    public async Task<HttpResponseMessage> AddQuestions(Guid? quizId, List<Question> questions)
    {
        return await _httpClient.PutAsJsonAsync($"/api/Quizzes/{quizId}/SetQuestions", questions);
    }
}