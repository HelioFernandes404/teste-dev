using Microsoft.AspNetCore.Mvc;

namespace CrawlerAPI.Interfaces;

public interface ICrawlersServices
{
    Task<ActionResult<object>> GetCrawlWebSite(string url);
}