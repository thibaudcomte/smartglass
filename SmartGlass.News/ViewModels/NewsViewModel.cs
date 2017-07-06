using Prism.Mvvm;
using SmartGlass.News.Models;
using SmartGlass.News.Service;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace SmartGlass.News.ViewModels
{
    internal class NewsViewModel : BindableBase
    {
        public ObservableCollection<NewsSource> NewsSources { get; }

        private readonly SynchronizationContext _SyncContext;

        public NewsViewModel(INewsService service)
        {
            _SyncContext = SynchronizationContext.Current;

            NewsSources = new ObservableCollection<NewsSource>();
            foreach (var category in service.NewsSources)
            {
                NewsSources.Add(category);
            }

            service.CategoriesUpdated += ServiceCategoriesUpdated;
        }

        private void ServiceCategoriesUpdated(object sender, EventArgs e)
        {
            _SyncContext.Post((o) =>
            {
                var service = sender as INewsService;

                NewsSources.Clear();
                foreach (var category in service.NewsSources)
                {
                    NewsSources.Add(category);
                }

            }, null);
        }
    }
}
