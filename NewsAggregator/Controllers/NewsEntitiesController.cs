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
        private readonly NewsRssFeedN _newsRssFeedN;
        private readonly NewsDbContext _context;

        public NewsEntitiesController(NewsDbContext context)
        {
            _newsRssFeed = new NewsRssFeed();
            _newsRssFeedN = new NewsRssFeedN();
            _context = context;
        }

        // GET: api/NewsEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsEntity>>> GetNews()
        {
            return await _context.News.ToListAsync();
        }

        // GET: api/NewsEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsEntity>> GetNewsEntity(int id)
        {
            var newsEntity = await _context.News.FindAsync(id);

            if (newsEntity == null)
            {
                return NotFound();
            }

            return newsEntity;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string rssUrl)
        {
            if (string.IsNullOrEmpty(rssUrl))
            {
                return BadRequest("Rss Обязателен!");
            }

            try
            {
                var rssItems = await _newsRssFeedN.ReadRssFeedAsyncN(rssUrl);
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
                return StatusCode(500, "An error occurred while saving the RSS feed to the database.");
            }
        }

    }
}
