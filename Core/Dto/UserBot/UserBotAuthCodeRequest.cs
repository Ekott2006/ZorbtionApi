using System.ComponentModel.DataAnnotations;
using Core.Model.Helper;

namespace Core.Dto.UserBot;

public class UserBotAuthCodeRequest
{
    [Required] public UserBotProviderType Type { get; set; }
}