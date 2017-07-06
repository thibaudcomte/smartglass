using System.Threading.Tasks;

namespace SmartGlass.Core.UI.Regions
{
    public interface IRegionActivationAware
    {
        Task OnBeforeActivatedAsync();
        Task OnAfterActivatedAsync();

        Task OnBeforeDeactivatedAsync();
        Task OnAfterDeactivatedAsync();
    }
}
