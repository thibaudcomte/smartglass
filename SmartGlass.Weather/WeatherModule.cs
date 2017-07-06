using Microsoft.Practices.Unity;
using SmartGlass.Core.Commanding;
using SmartGlass.Core.Module;
using SmartGlass.Core.UI.Regions;
using SmartGlass.Weather.Service;
using SmartGlass.Weather.Settings;
using SmartGlass.Weather.Views;
using SmartGlass.Weather.Voice;
using System;
using System.Threading;

namespace SmartGlass.Weather
{
    public class WeatherModule : IModule
    {
        private readonly IRegionManager _RegionManager;
        private readonly IUnityContainer _Container;
        private WeatherVoiceCommandProcessor _VoiceCommandProcessor;

        private readonly Timer _Timer;

        public WeatherModule(IRegionManager regionManager, IUnityContainer container)
        {
            _RegionManager = regionManager;
            _Container = container;

            _Timer = new Timer(TimerCallback, null, -1, Timeout.Infinite);
        }

        public void Initialize()
        {
            _Container.RegisterType<IWeatherSettings, WeatherSettings>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<IWeatherService, WeatherService>(new ContainerControlledLifetimeManager());

            _VoiceCommandProcessor = new WeatherVoiceCommandProcessor(_RegionManager, _Container.Resolve<IWeatherService>());

            ViewNames.WeatherCurrentViewName = _RegionManager.RegisterRegionView(ERegionLocation.TopLeft,
                _Container.Resolve<WeatherCurrentView>());

            _RegionManager.ActivateRegionViewAsync(ERegionLocation.TopLeft, 
                ViewNames.WeatherCurrentViewName);

            ViewNames.WeatherForecastsViewName = _RegionManager.RegisterRegionView(ERegionLocation.Center,
                _Container.Resolve<WeatherForecastsView>());

            _Timer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(30));
        }

        public IVoiceCommandProcessor GetVoiceCommandProcessor()
        {
            return _VoiceCommandProcessor;
        }

        public void Dispose()
        {
            _Timer?.Dispose();
        }

        private async void TimerCallback(object state = null)
        {
            await _Container.Resolve<IWeatherService>().UpdateAsync();
        }
    }
}
