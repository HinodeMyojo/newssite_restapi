using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using NewsAggregator.Models;
using System.Xml;

namespace NewsAggregator.RssReader
{
    public class NewsRssFeed
    {
        public async Task<IEnumerable<RssItem>> ReadRssFeedAsync(string url)
        {
            var rssItems = new List<RssItem>();

            using (var xmlReader = XmlReader.Create(url, new XmlReaderSettings { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    if (feedReader.ElementType == SyndicationElementType.Item)
                    {
                        ISyndicationItem item = await feedReader.ReadItem();
                        var firstLink = item.Links.FirstOrDefault();
                        rssItems.Add(new RssItem
                        {
                            Title = item.Title,
                            Link = firstLink?.Uri.ToString(),
                            Description = item.Description,
                            PublishDate = item.Published.UtcDateTime
                        });
                    }
                }
            }

            return rssItems;
        }
    }
}
