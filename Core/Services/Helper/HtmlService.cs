using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Core.Services.Helper.Interface;

namespace Core.Services.Helper;

public class HtmlService : IHtmlService
{
    public async Task<string?> Parse(string html)
    {
        HtmlParser htmlParser = new();
        IHtmlDocument document = await htmlParser.ParseDocumentAsync(html);
        return document.Body?.ToHtml();
    }
}