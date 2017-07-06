using System;

namespace SmartGlass.News.Models
{
    public class NewsEntry
    {
        public string Title { get; }
        public string Subtitle { get; }
        public DateTimeOffset PubDateTime { get; }

        public NewsEntry(string title, string subtitle, DateTimeOffset pubDateTime)
        {
            Title = title;
            Subtitle = subtitle;
            PubDateTime = pubDateTime;
        }
    }
}
