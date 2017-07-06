using Microsoft.Practices.Unity;
using SmartGlass.Core.Commanding;
using SmartGlass.Core.Module;
using SmartGlass.Core.UI.Regions;
using SmartGlass.Radio.Service;
using SmartGlass.Radio.Settings;
using SmartGlass.Radio.Views;
using SmartGlass.Radio.Voice;

namespace SmartGlass.Radio
{
    public class RadioModule : IModule
    {
        private readonly IRegionManager _RegionManager;
        private readonly IUnityContainer _Container;

        public RadioModule(IRegionManager regionManager, IUnityContainer container)
        {
            _RegionManager = regionManager;
            _Container = container;
        }

        public void Initialize()
        {
            _Container.RegisterType<IRadioSettings, RadioSettings>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<IRadioService, RadioService>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<RadioVoiceCommandProcessor>(new ContainerControlledLifetimeManager());

            ViewNames.RadioViewName = _RegionManager.RegisterRegionView(ERegionLocation.Center,
                _Container.Resolve<RadioView>());
        }

        public IVoiceCommandProcessor GetVoiceCommandProcessor()
        {
            return _Container.Resolve<RadioVoiceCommandProcessor>();
        }

        public void Dispose()
        {
            return;
        }
    }
}
