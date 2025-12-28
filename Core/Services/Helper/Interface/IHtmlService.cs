namespace Core.Services.Helper;

public interface IHtmlService
{
    Task<string?> Parse(string html);
}