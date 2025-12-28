using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{

    [HttpGet]
    public GetMessage Get()
    {
        return new GetMessage("Welcome to Zorbtion Flashcard API");
    }

    public record GetMessage(string Message);
}