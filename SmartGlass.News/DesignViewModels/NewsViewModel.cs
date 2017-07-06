using Prism.Mvvm;
using SmartGlass.News.Models;
using System;
using System.Collections.ObjectModel;

namespace SmartGlass.News.DesignViewModels
{
    internal class NewsViewModel : BindableBase
    {
        public ObservableCollection<NewsSource> NewsSources { get; }

        public NewsViewModel()
        {
            NewsSources = new ObservableCollection<NewsSource>();

            var category = new NewsSource("source one");
            category.AddNewsEntry(new NewsEntry("title one", "subtitle one qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq", DateTimeOffset.Now));
            category.AddNewsEntry(new NewsEntry("title two", "subtitle two qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq", DateTimeOffset.Now));
            NewsSources.Add(category);

            category = new NewsSource("source two");
            category.AddNewsEntry(new NewsEntry("title one", "subtitle one qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq", DateTimeOffset.Now));
            category.AddNewsEntry(new NewsEntry("title two", "subtitle two qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq", DateTimeOffset.Now));
            NewsSources.Add(category);

        }
    }
}
