namespace NewsAggregator.Models
{
    public class NewsEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required string SourceUrl { get; set; }
    }
}
