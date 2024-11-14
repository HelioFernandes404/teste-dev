using CrawlerAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrawlerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class CrawlerController(ICrawlersServices _services) : ControllerBase
{

    [HttpPost]
    public Task<ActionResult<object>> Crawl(string url)
    {
        var result = _services.GetCrawlWebSite(url);

        return result;
    }
    
}