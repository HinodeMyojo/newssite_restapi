using Microsoft.EntityFrameworkCore;

namespace NewsAggregator.Models
{
    public class NewsDbContext: DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();  
        }

        public DbSet<NewsEntity> News { get; set;}
    }
}
