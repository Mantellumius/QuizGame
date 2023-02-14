using QuizGame.Shared.Models;

namespace QuizGame.Client.ViewModels;

public class AnswerViewModel
{
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
    public bool IsSelected { get; set; }

    public static AnswerViewModel CreateFromModel(Answer answer)
    {
        return new()
            { Text = answer.Text, IsCorrect = answer.IsCorrect, IsSelected = false };
    }
}