using System.Collections.Generic;

namespace SmartGlass.News.Models
{
    public class NewsSource
    {
        public string Name { get; }

        private List<NewsEntry> _Entries;
        public IEnumerable<NewsEntry> Entries => _Entries;

        public NewsSource(string name)
        {
            Name = name;
            _Entries = new List<NewsEntry>();
        }

        public void AddNewsEntry(NewsEntry entry)
        {
            _Entries.Add(entry);
        }
    }
}
