using Core.Model;

namespace Core.Dto.User;

public class UpdateUserRequest
{
    public DeckOption DeckOption { get; set; }
    public List<UserAiProvider>  AiProviders { get; set; } = [];
}