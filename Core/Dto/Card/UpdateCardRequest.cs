using System.ComponentModel.DataAnnotations;

namespace Core.Dto.Card;

public class UpdateCardRequest
{
    [Required] public UpdateCardStateRequest State { get; set; }
}