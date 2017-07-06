using Microsoft.Practices.Unity;
using SmartGlass.Core.Commanding;
using SmartGlass.Core.Module;
using SmartGlass.Core.UI.Regions;
using SmartGlass.News.Providers;
using SmartGlass.News.Service;
using SmartGlass.News.Views;
using SmartGlass.News.Voice;
using System;
using System.Threading;

namespace SmartGlass.News
{
    public class NewsModule : IModule
    {
        private readonly IRegionManager _RegionManager;
        private readonly IUnityContainer _Container;

        private readonly Timer _Timer;

        public NewsModule(IRegionManager regionManager, IUnityContainer container)
        {
            _RegionManager = regionManager;
            _Container = container;

            _Timer = new Timer(TimerCallback, null, -1, Timeout.Infinite);
        }

        public void Initialize()
        {
            var service = new NewsService(new France24NewsProvider(), new ZDNetNewsProvider());
            _Container.RegisterInstance<INewsService>(service, new ContainerControlledLifetimeManager());
            _Container.RegisterType<NewsVoiceCommandProcessor>(new ContainerControlledLifetimeManager());

            ViewNames.NewsViewName = _RegionManager.RegisterRegionView(ERegionLocation.Center,
                _Container.Resolve<NewsView>());

            _Timer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(15));
        }

        public IVoiceCommandProcessor GetVoiceCommandProcessor()
        {
            return _Container.Resolve<NewsVoiceCommandProcessor>();
        }

        public void Dispose()
        {
            _Timer?.Dispose();
        }

        private async void TimerCallback(object state = null)
        {
            await _Container.Resolve<INewsService>().UpdateAsync();
        }
    }
}
