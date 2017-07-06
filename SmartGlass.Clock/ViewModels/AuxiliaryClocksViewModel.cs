using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace SmartGlass.Clock.ViewModels
{
    internal class AuxiliaryClocksViewModel : BindableBase
    {
        public ObservableCollection<ClockInfo> AuxiliaryClocksInfo { get; }

        public AuxiliaryClocksViewModel(IClockService clockService)
        {
            AuxiliaryClocksInfo = new ObservableCollection<ClockInfo>();

            foreach (var ci in clockService.AuxiliaryClocksInfo)
            {
                AuxiliaryClocksInfo.Add(ci);
            }
        }
    }
}
