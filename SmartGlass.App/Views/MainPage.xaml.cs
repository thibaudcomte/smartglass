using SmartGlass.Core.UI.Regions;
using Windows.UI.Xaml.Controls;

namespace SmartGlass.App.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage(IRegionManager regionManager)
        {
            InitializeComponent();

            regionManager.SetLayoutRoot(layoutRoot);

            regionManager.CreateRegionLocation(ERegionLocation.TopLeft, TopLeftCC);
            regionManager.CreateRegionLocation(ERegionLocation.TopRight, TopRightCC);
            regionManager.CreateRegionLocation(ERegionLocation.Center, CenterCC);
        }
    }
}
