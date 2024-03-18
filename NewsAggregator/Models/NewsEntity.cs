namespace NewsAggregator.Models
{
    public class NewsEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset PubDate { get; set; }
        public string SourceUrl { get; set; } = string.Empty;
    }
}
