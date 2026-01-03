using System.Security.Claims;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserBotCodeController(IUserBotCodeService userBotCodeService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserBotAuthCodeResponse>> Create()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Forbid();

        string code = await userBotCodeService.GenerateCode(userId);
        return Ok(new UserBotAuthCodeResponse(code));
    }

    public record UserBotAuthCodeResponse(string Code);
}