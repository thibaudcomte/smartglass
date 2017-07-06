using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SmartGlass.Core.UI.Regions
{
    public interface IRegionManager
    {
        void CreateRegionLocation(ERegionLocation regionLocation, ContentControl contentControl);
        string RegisterRegionView(ERegionLocation regionLocation, FrameworkElement view);
        Task ActivateRegionViewAsync(ERegionLocation regionLocation, string token);
        Task DeactivateRegionAsync(ERegionLocation regionLocation);

        void SetLayoutRoot(Panel layoutRoot);
        void SetLayoutRootVisibility(bool visible);
    }
}
