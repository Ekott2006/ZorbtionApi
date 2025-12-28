namespace Core.Services.Helper;

public interface ICssService
{
    Task<string> Parse(string html);
}