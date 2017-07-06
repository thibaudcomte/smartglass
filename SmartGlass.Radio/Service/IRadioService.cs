using SmartGlass.Radio.Models;
using System;
using System.Collections.Generic;

namespace SmartGlass.Radio.Service
{
    public interface IRadioService
    {
        IReadOnlyList<RadioChannel> RadioChannels { get; }

        RadioChannel SelectedRadioChannel { get; set; }
        event EventHandler<RadioChannel> SelectedRadioChannelChanged;

        bool IsPlaying { get; set; }
        event EventHandler<bool> RadioPlaybackStatusChanged;

        double Volume { get; set; }
        event EventHandler<double> RadioPlaybackVolumeChanged;
    }
}