using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Models;
using NewsAggregator.RssReader;

namespace NewsAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsEntitiesController : ControllerBase
    {
        private readonly NewsRssFeed _newsRssFeed;
        private readonly NewsDbContext _context;

        public NewsEntitiesController(NewsDbContext context)
        {
            _newsRssFeed = new NewsRssFeed();
            _context = context;
        }

        // GET: api/NewsEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsEntity>>> GetFilterNews(string? title, string? body)
        {
            var query = _context.News.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(n => n.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(body))
            {
                query = query.Where(n => n.Description.Contains(body));
            }

            var filteredNews = await query.ToListAsync();

            return Ok(filteredNews);
        }

        // POST: api/NewsEntities
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string rssUrl)
        {
            if (string.IsNullOrEmpty(rssUrl))
            {
                return BadRequest("Rss Обязателен!");
            }

            try
            {
                var rssItems = await _newsRssFeed.ReadRssFeedAsync(rssUrl);
                var entities = rssItems.Select(item => new NewsEntity
                {
                    Title = item.Title,
                    Link = item.Link,
                    Description = item.Description,
                    SourceUrl = rssUrl,
                    PubDate = item.PubDate.ToUniversalTime(),
                }).ToList();

                await _context.News.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return Ok(entities);
            }
            catch (System.Exception ex)
            {
                //добавить в будущем работу с ошибками
                return StatusCode(500, "Произошла ошибки при сохрании RSS в базу данных.");
            }
        }

    }
}
