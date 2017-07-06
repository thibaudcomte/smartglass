using Microsoft.Practices.Unity;
using SmartGlass.Clock.Views;
using SmartGlass.Clock.Voice;
using SmartGlass.Core.Commanding;
using SmartGlass.Core.Module;
using SmartGlass.Core.UI.Regions;

namespace SmartGlass.Clock
{
    public class ClockModule : IModule
    {
        private readonly IRegionManager _RegionManager;
        private readonly IUnityContainer _Container;

        public ClockModule(IRegionManager regionManager, IUnityContainer container)
        {
            _RegionManager = regionManager;
            _Container = container;
        }

        public void Initialize()
        {
            _Container.RegisterType<IClockSettings, ClockSettings>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<IClockService, ClockService>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<ClockVoiceCommandProcessor>(new ContainerControlledLifetimeManager());

            ViewNames.MainClockViewName = _RegionManager.RegisterRegionView(ERegionLocation.TopRight,
                _Container.Resolve<MainClockView>());

            _RegionManager.ActivateRegionViewAsync(ERegionLocation.TopRight, ViewNames.MainClockViewName);

            ViewNames.AuxiliaryClocksViewName = _RegionManager.RegisterRegionView(ERegionLocation.Center,
                _Container.Resolve<AuxiliaryClocksView>());
        }

        public IVoiceCommandProcessor GetVoiceCommandProcessor()
        {
            return _Container.Resolve<ClockVoiceCommandProcessor>();
        }

        public void Dispose()
        {
            _Container.Resolve<ClockService>().Dispose();
        }
    }
}
