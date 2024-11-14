using CrawlerAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrawlerAPI.Services;

public class CrawlersServices : ICrawlersServices
{
    public Task<ActionResult<object>> GetCrawlWebSite(string url)
    {
        throw new NotImplementedException();
        
        //TODO: 1. Encontrar todos os links da Pagina
        
        
        //TODO: 2. Visitar cada link encontrado
        //TODO: 3. Repetir o processo para cada nova página
        //TODO: 4. Retornar todos os links encontrados
        
        
    }
}