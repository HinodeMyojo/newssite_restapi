using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using NewsAggregator.Models;
using System.Security.Policy;
using System.Xml;

namespace NewsAggregator.RssReader
{
    public class NewsRssFeedN
    {
        public async Task<IEnumerable<NewsEntity>> ReadRssFeedAsyncN(string originurl)
        {
            var rssItems = new List<NewsEntity>();
            var url = FindRss.CheckUrl(originurl);

            using (var xmlReader = XmlReader.Create(url, new XmlReaderSettings { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    if (feedReader.ElementType == SyndicationElementType.Item)
                    {
                        ISyndicationItem item = await feedReader.ReadItem();
                        var firstLink = item.Links.FirstOrDefault();
                        rssItems.Add(new NewsEntity
                        {
                            Title = item.Title,
                            Link = firstLink?.Uri.ToString(),
                            Description = item.Description,
                            PubDate = item.Published
                            
                        });
                    }
                }
            }

            return rssItems;
        }
    }
}
