using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace SmartGlass.Clock.DesignViewModels
{
    internal class AuxiliaryClocksViewModel : BindableBase
    {
        public ObservableCollection<ClockInfo> AuxiliaryClocksInfo { get; }
            = new ObservableCollection<ClockInfo> { new ClockInfo(), new ClockInfo() };
    }
}
