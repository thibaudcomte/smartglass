using Prism.Mvvm;

namespace SmartGlass.Clock.ViewModels
{
    internal class MainClockViewModel : BindableBase
    {
        public ClockInfo MainClockInfo { get; }

        public MainClockViewModel(IClockService clockService)
        {
            MainClockInfo = clockService.MainClockInfo;
        }
    }
}
