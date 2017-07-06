using SmartGlass.News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace SmartGlass.News.Providers
{
    abstract class AbstractRssNewsProvider : INewsProvider
    {
        public string Name { get; }
        private readonly string _RssUri;
        private readonly int _MaxCount;

        public AbstractRssNewsProvider(string name, string rssUri, int maxCount)
        {
            Name = name;
            _RssUri = rssUri;
            _MaxCount = maxCount;
        }

        public async Task<IEnumerable<NewsEntry>> GetNewsEntriesAsync()
        {
            var client = new SyndicationClient();
            var feeds = await client.RetrieveFeedAsync(new Uri(_RssUri));

            return feeds.Items.Take(Math.Min(_MaxCount, feeds.Items.Count)).Select(
                item => new NewsEntry(item.Title.Text, item.Summary.Text, item.PublishedDate));
        }
    }
}
