using System.Security.Claims;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserBotProviderController(IUserBotProviderService userBotProviderService) : ControllerBase
{
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Forbid();

        int deletedCount = await userBotProviderService.Delete(userId, id);
        if (deletedCount == 0) return NotFound();

        return NoContent();
    }
}