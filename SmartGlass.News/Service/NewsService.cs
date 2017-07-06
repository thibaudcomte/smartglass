using SmartGlass.News.Models;
using SmartGlass.News.Providers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGlass.News.Service
{
    public class NewsService : INewsService
    {
        private List<NewsSource> _NewsSources;
        public IEnumerable<NewsSource> NewsSources => _NewsSources;

        public event EventHandler CategoriesUpdated;

        private INewsProvider[] _Providers;

        public NewsService(params INewsProvider[] providers)
        {
            _Providers = providers;
            _NewsSources = new List<NewsSource>();
        }

        public async Task UpdateAsync()
        {
            _NewsSources.Clear();

            foreach (var provider in _Providers)
            {
                var category = new NewsSource(provider.Name);
                foreach (var entry in await provider.GetNewsEntriesAsync())
                {
                    category.AddNewsEntry(entry);
                }
                _NewsSources.Add(category);
            }

            CategoriesUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
