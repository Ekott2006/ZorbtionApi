using Core.Model;

namespace Core.Services.Helper;

using System.Security.Claims;

public class AiServiceFactory
{
    public static IAiService? GetUserService(UserAiProvider provider)
    {
        return provider.Type switch
        {
            UserAiProviderType.ChatGpt => new OpenAiService(provider.Key),
            UserAiProviderType.Gemini => new GeminiService(provider.Key),
            _ => null
        };
    }
}