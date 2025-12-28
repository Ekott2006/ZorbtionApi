using Core.Model.Helper;

namespace Core.Dto.Card;

public record CardResponse(int Id, CardState State, double EaseFactor, string Front, string Back);