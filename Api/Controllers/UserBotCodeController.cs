using System.Security.Claims;
using Core.Dto.UserBot;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserBotCodeController(IUserBotCodeService userBotCodeService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserBotAuthCodeResponse>> Create(UserBotAuthCodeRequest request)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Forbid();

        string code = await userBotCodeService.GenerateAuthCode(userId, request);
        return Ok(new UserBotAuthCodeResponse(code));
    }

    public record UserBotAuthCodeResponse(string Code);
}