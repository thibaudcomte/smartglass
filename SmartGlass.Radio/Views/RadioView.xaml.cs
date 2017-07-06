using SmartGlass.Core.UI.Regions;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SmartGlass.Radio.Views
{
    public sealed partial class RadioView : UserControl, IRegionActivationAware
    {
        public RadioView()
        {
            InitializeComponent();
        }

        public Task OnBeforeActivatedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnAfterActivatedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnBeforeDeactivatedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnAfterDeactivatedAsync()
        {
            return Task.CompletedTask;
        }
    }
}
