﻿namespace NewsAggregator.Models
{
    public class RssItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PublishDate { get; set; }
    }
}