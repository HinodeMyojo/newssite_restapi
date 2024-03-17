using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NewsEntity>> PostNewsEntity(NewsEntity newsEntity)
        {
            _context.News.Add(newsEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsEntity", new { id = newsEntity.Id }, newsEntity);
        }

        private bool NewsEntityExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
