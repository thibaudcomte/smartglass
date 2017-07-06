using Microsoft.Practices.Unity;
using Prism.Unity.Windows;
using SmartGlass.App.Views;
using SmartGlass.App.Voice;
using SmartGlass.Core.Commanding;
using SmartGlass.Core.Module;
using SmartGlass.Core.UI.Regions;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SmartGlass.App
{
    sealed partial class App : PrismUnityApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
        }

        protected override UIElement CreateShell(Frame rootFrame)
        {
            return Container.Resolve<MainPage>();
        }

        protected async override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            var moduleManager = Container.Resolve<ModuleManager>();
            await moduleManager.LoadModulesFromAssembliesAsync();
            moduleManager.InitializeModules();
            await moduleManager.RegisterCommandProcessorsAsync();

            var commander = Container.Resolve<VoiceCommander>();
            await commander.RegisterCommandProcessorAsync(Container.Resolve<MainPageVoiceCommandProcessor>());
            await commander.BeginAsync();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IRegionManager, RegionManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<VoiceCommander>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new Language("en-CA")));
            Container.RegisterType<ModuleManager>(new ContainerControlledLifetimeManager());
        }
    }
}
