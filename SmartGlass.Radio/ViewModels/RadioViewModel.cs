using Prism.Mvvm;
using SmartGlass.Radio.Models;
using SmartGlass.Radio.Service;
using System.Collections.ObjectModel;
using System.Threading;

namespace SmartGlass.Radio.ViewModels
{
    internal class RadioViewModel : BindableBase
    {
        public ObservableCollection<RadioChannel> Radios { get; }

        private RadioChannel _ActiveRadio;
        public RadioChannel ActiveRadio
        {
            get { return _ActiveRadio; }
            set { SetProperty(ref _ActiveRadio, value); }
        }

        private bool _IsPlaying;
        public bool IsPlaying
        {
            get { return _IsPlaying; }
            set { SetProperty(ref _IsPlaying, value); }
        }

        private double _Volume;
        public double Volume
        {
            get { return _Volume; }
            set { SetProperty(ref _Volume, value); }
        }

        private readonly SynchronizationContext _SyncContext;

        public RadioViewModel(IRadioService radioService)
        {
            _SyncContext = SynchronizationContext.Current;

            Radios = new ObservableCollection<RadioChannel>();

            foreach (var radio in radioService.RadioChannels)
            {
                Radios.Add(radio);
            }

            ActiveRadio = radioService.SelectedRadioChannel;
            IsPlaying = radioService.IsPlaying;
            Volume = radioService.Volume;

            radioService.SelectedRadioChannelChanged += RadioService_RadioSelectionChanged;
            radioService.RadioPlaybackStatusChanged += RadioService_RadioPlaybackStatusChanged;
            radioService.RadioPlaybackVolumeChanged += RadioService_VolumeChanged;
        }

        private void RadioService_VolumeChanged(object sender, double volume)
        {
            _SyncContext.Post((o) => { Volume = volume; }, null);
        }

        private void RadioService_RadioPlaybackStatusChanged(object sender, bool playing)
        {
            _SyncContext.Post((o) => { IsPlaying = playing; }, null);
        }

        private void RadioService_RadioSelectionChanged(object sender, RadioChannel radio)
        {
            _SyncContext.Post((o) => { ActiveRadio = radio; }, null);
        }
    }
}
