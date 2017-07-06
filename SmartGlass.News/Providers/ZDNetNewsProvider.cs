namespace SmartGlass.News.Providers
{
    internal class ZDNetNewsProvider : AbstractRssNewsProvider
    {
        const string RssFeed = "http://www.zdnet.com/topic/microsoft/rss.xml";

        public ZDNetNewsProvider() 
            : base("ZDNet", RssFeed, 4)
        {
        }
    }
}
