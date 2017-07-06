using Microsoft.Practices.Unity;
using SmartGlass.Core.Commanding;
using SmartGlass.Core.Module;
using SmartGlass.Core.UI.Regions;
using SmartGlass.Video.Service;
using SmartGlass.Video.Settings;
using SmartGlass.Video.Views;
using SmartGlass.Video.Voice;

namespace SmartGlass.Video
{
    public class VideoModule : IModule
    {
        private readonly IRegionManager _RegionManager;
        private readonly IUnityContainer _Container;

        public VideoModule(IRegionManager regionManager, IUnityContainer container)
        {
            _RegionManager = regionManager;
            _Container = container;
        }

        public void Initialize()
        {
            _Container.RegisterType<IVideoSettings, VideoSettings>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<IVideoService, VideoService>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<VideoVoiceCommandProcessor>(new ContainerControlledLifetimeManager());

            ViewNames.VideoViewName = _RegionManager.RegisterRegionView(ERegionLocation.Center,
                _Container.Resolve<VideoView>());
        }

        public IVoiceCommandProcessor GetVoiceCommandProcessor()
        {
            return _Container.Resolve<VideoVoiceCommandProcessor>();
        }

        public void Dispose()
        {
            return;
        }
    }
}
