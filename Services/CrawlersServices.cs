using CrawlerAPI.Interfaces;
using CrawlerAPI.Model;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace CrawlerAPI.Services;

public class CrawlersServices : ICrawlersServices
{
    static HashSet<string> visitedUrls = new HashSet<string>();
    private List<string> CrawlersWeb = new List<string>();
    static int maxDepth = 3;
    
    public async Task<CrawlerResponse> GetCrawlWebSite(string startUrl, int depth, int maxPagesToSearch)
    {
        visitedUrls.Clear(); 
        CrawlersWeb.Clear();
        await Crawl(startUrl, 0, maxPagesToSearch);

        return new CrawlerResponse
        {
            LinksFounds = CrawlersWeb.Take(maxPagesToSearch).Select(hypelinks => hypelinks),
            MoreLinksFounds = CrawlersWeb.Count()
        };
    }

    async Task Crawl(string url, int depth, int maxPagesToSearch)
    {
        // Verifica se o limite de páginas foi atingido antes de qualquer processamento
        if (CrawlersWeb.Count >= maxPagesToSearch) return;

        // Verifica profundidade e se a URL já foi visitada
        if (visitedUrls.Contains(url) || depth > maxDepth) return;

        visitedUrls.Add(url);

        try
        {
            HttpClient client = new HttpClient();
            
            var response = await client.GetStringAsync(url);
            
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);
            
            var links = doc.DocumentNode.SelectNodes("//a[@href]");

            if (links != null)
            {
                foreach (var link in links)
                {
                    // Verifica o limite de páginas antes de cada nova adição ao CrawlersWeb e saída do loop
                    if (visitedUrls.Count >= maxPagesToSearch) break;

                    string href = link.GetAttributeValue("href", string.Empty);

                    if (Uri.IsWellFormedUriString(href, UriKind.Relative))
                    {
                        Uri baseUri = new Uri(url);
                        href = new Uri(baseUri, href).ToString();
                    }

                    Console.WriteLine($"[{depth}] Encontrado link: {href}");
                    CrawlersWeb.Add(href);

                    // Continua o rastreamento para o próximo link, aumentando a profundidade
                    await Crawl(href, depth + 1, maxPagesToSearch);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar a página {url}: {ex.Message}");
        }
    }
}
