namespace Core.Services.Helper;

public interface ITemplateService
{
    Task<string> Parse(string template, Dictionary<string, string> data);
    List<string> GetAllFields(IEnumerable<string> templates);
}