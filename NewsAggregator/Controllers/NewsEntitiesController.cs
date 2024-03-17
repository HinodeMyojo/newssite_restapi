using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SyndicationFeed;
using NewsAggregator.Models;

namespace NewsAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsEntitiesController : ControllerBase
    {
        private readonly NewsDbContext _context;

        public NewsEntitiesController(NewsDbContext context)
        {
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

        // POST: api/NewsEntities
        [HttpPost("FetchNewsFromUrlOrRss")]
        public async Task<IActionResult> FetchNewsFromUrl([FromBody] string newsUrl)
        {
            try
            {
                var newsItems = await RssReader.RssReadFeed.CreateRssFeedReader(newsUrl);

                // После получения новостей, они сохраняются в базу данных
                foreach (var newsItem in newsItems)
                {
                    _context.News.Add(newsItem);
                }
                await _context.SaveChangesAsync();
                return Ok(new { message = "News fetched and saved successfully." });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
