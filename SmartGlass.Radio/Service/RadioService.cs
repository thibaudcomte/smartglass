using SmartGlass.Radio.Models;
using SmartGlass.Radio.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartGlass.Radio.Service
{
    public class RadioService : IRadioService
    {
        private readonly List<RadioChannel> _RadioChannels;
        public IReadOnlyList<RadioChannel> RadioChannels => _RadioChannels;

        private RadioChannel _SelectedRadioChannel;
        public RadioChannel SelectedRadioChannel
        {
            get { return _SelectedRadioChannel; }
            set
            {
                if (_SelectedRadioChannel != value)
                {
                    _SelectedRadioChannel = value;
                    SelectedRadioChannelChanged?.Invoke(this, value);
                }
            }
        }

        private bool _IsPlaying;
        public bool IsPlaying
        {
            get { return _IsPlaying; }
            set
            {
                if (_IsPlaying != value)
                {
                    _IsPlaying = value;
                    RadioPlaybackStatusChanged?.Invoke(this, value);
                }
            }
        }

        private double _Volume;
        public double Volume
        {
            get { return _Volume; }
            set
            {
                if (_Volume != value)
                {
                    _Volume = value;
                    RadioPlaybackVolumeChanged?.Invoke(this, value);
                }
            }
        }

        public event EventHandler<RadioChannel> SelectedRadioChannelChanged;
        public event EventHandler<bool> RadioPlaybackStatusChanged;
        public event EventHandler<double> RadioPlaybackVolumeChanged;

        public RadioService(IRadioSettings settings)
        {
            _RadioChannels = new List<RadioChannel>();
            _RadioChannels.AddRange(settings.RadioChannels);

            SelectedRadioChannel = _RadioChannels.First();

            Volume = 0.5;
        }
    }
}
