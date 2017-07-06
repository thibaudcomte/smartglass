using SmartGlass.Core.UI.Regions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SmartGlass.Clock.Views
{
    public sealed partial class AuxiliaryClocksView : UserControl, IRegionActivationAware
    {
        public AuxiliaryClocksView()
        {
            InitializeComponent();
        }

        public Task OnBeforeActivatedAsync()
        {
            VisualStateManager.GoToState(this, "Invisible", false);
            return Task.CompletedTask;
        }

        public Task OnAfterActivatedAsync()
        {
            VisualStateManager.GoToState(this, "Visible", true);
            return Task.CompletedTask;
        }

        public async Task OnBeforeDeactivatedAsync()
        {
            VisualStateManager.GoToState(this, "Invisible", true);
            await Task.Delay(1000);
        }

        public Task OnAfterDeactivatedAsync()
        {
            return Task.CompletedTask;
        }
    }
}
