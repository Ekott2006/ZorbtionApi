using System.Security.Claims;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserBotController(IUserBotService userBotService) : ControllerBase
{
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Forbid();

        int deletedCount = await userBotService.Delete(userId, id);
        if (deletedCount == 0) return NotFound();

        return NoContent();
    }
}