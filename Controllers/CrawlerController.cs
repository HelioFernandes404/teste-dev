using CrawlerAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrawlerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class CrawlerController(ICrawlersServices services) : ControllerBase
{
    [HttpGet]
    public Task<string> Get()
    {
        return services.Hello();
    }
    
    
}