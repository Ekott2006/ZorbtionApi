namespace Core.Services.Helper;

public class GeminiService(string key) : IAiService
{
    public Task<List<Dictionary<string, string>>?> GenerateFlashcard(List<string> fields, string description)
    {
        throw new NotImplementedException();
    }

    public Task<int?> CheckAnswer(string question, string userAnswer, string correctAnswer)
    {
        throw new NotImplementedException();
    }
}