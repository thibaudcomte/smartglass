namespace SmartGlass.News.Providers
{
    internal class France24NewsProvider : AbstractRssNewsProvider
    {
        const string RssFeed = "http://www.france24.com/en/top-stories/rss";

        public France24NewsProvider() 
            : base("France 24", RssFeed, 4)
        {
        }
    }
}
