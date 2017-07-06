using Prism.Mvvm;

namespace SmartGlass.Clock.DesignViewModels
{
    internal class MainClockViewModel : BindableBase
    {
        public ClockInfo MainClockInfo { get; } = new ClockInfo();
    }
}
