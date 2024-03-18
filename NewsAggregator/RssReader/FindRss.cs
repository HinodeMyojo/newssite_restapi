using HtmlAgilityPack;
using System.Net;

namespace NewsAggregator.RssReader
{

    public class FindRss
    {
        static string CheckContentType(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                string contentType = response.ContentType;
                return contentType;
            }
        }

        public static string CheckUrl(string url)
        {
            var contenttype = CheckContentType(url);
            if (contenttype.Contains("application/rss+xml"))
            {
                return url;
            }
            else
            {
                var newUrl = FindRssFeedUrl(url);
                return newUrl;
            }

        }

        public static string FindRssFeedUrl(string sourceUrl)
        {
            var web = new HtmlWeb(); 
            var doc = web.Load(sourceUrl);
            var head = doc.DocumentNode.SelectSingleNode("//head");
            var linkTags = head.SelectNodes(".//link[@type='application/rss+xml']");

            if (linkTags != null)
            {
                foreach (var link in linkTags) 
                {
                    var href = link.GetAttributeValue("href", string.Empty);
                    if (!string.IsNullOrEmpty(href))
                    {
                        return href;
                    }
                }
            }
            return null;
        }
    }

}
