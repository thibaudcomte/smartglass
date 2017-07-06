using Prism.Mvvm;
using SmartGlass.Radio.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace SmartGlass.Radio.DesignViewModels
{
    internal class RadioViewModel : BindableBase
    {
        public ObservableCollection<RadioChannel> Radios { get; }

        public RadioChannel ActiveRadio { get; }

        public bool IsPlaying { get; }

        public double Volume { get; }

        public RadioViewModel()
        {
            Radios = new ObservableCollection<RadioChannel>();

            Radios.Add(new RadioChannel("Radio 1", null));
            Radios.Add(new RadioChannel("Radio 2222222", null));
            Radios.Add(new RadioChannel("Radio 3", null));
            Radios.Add(new RadioChannel("Radio 4", null));

            ActiveRadio = Radios.First();
        }
    }
}
