using Microsoft.SyndicationFeed.Rss;
using Microsoft.SyndicationFeed;
using System.Xml;

namespace NewsAggregator.RssReader
{
    class RssReadFeed
    {
        // Read an RssFeed
        public static async Task<List<ISyndicationItem>> CreateRssFeedReader(string filePath)
        {
            List<ISyndicationItem> items = new List<ISyndicationItem>();

            using (var xmlReader = XmlReader.Create(filePath, new XmlReaderSettings() { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    switch (feedReader.ElementType)
                    {
                        // Read category
                        case SyndicationElementType.Category:
                            ISyndicationCategory category = await feedReader.ReadCategory();
                            break;

                        // Read Image
                        case SyndicationElementType.Image:
                            ISyndicationImage image = await feedReader.ReadImage();
                            break;

                        // Read Item
                        case SyndicationElementType.Item:
                            ISyndicationItem item = await feedReader.ReadItem();
                            items.Add(item);
                            break;

                        // Read link
                        case SyndicationElementType.Link:
                            ISyndicationLink link = await feedReader.ReadLink();
                            break;

                        // Read Person
                        case SyndicationElementType.Person:
                            ISyndicationPerson person = await feedReader.ReadPerson();
                            break;

                        // Read content
                        default:
                            ISyndicationContent content = await feedReader.ReadContent();
                            break;
                    }
                }
            }
            return items;
        }
    }
}
