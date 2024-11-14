using CrawlerAPI.Interfaces;

namespace CrawlerAPI.Services;

public class CrawlersServices : ICrawlersServices
{

    public Task<string> Hello()
    {
        return Task.FromResult("Hello");
    }
}