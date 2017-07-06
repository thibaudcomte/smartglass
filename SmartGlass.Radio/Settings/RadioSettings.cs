using SmartGlass.Radio.Models;
using System;
using System.Collections.Generic;

namespace SmartGlass.Radio.Settings
{
    public class RadioSettings : IRadioSettings
    {
        private List<RadioChannel> _RadiosChannels;
        public IEnumerable<RadioChannel> RadioChannels => _RadiosChannels;

        public RadioSettings()
        {
            _RadiosChannels = new List<RadioChannel>();
            _RadiosChannels.Add(new RadioChannel("CBC News Hourly Edition", new Uri("http://podcast.cbc.ca/mp3/hourlynews.mp3")));
            _RadiosChannels.Add(new RadioChannel("BBC News", new Uri("http://wsdownload.bbc.co.uk/worldservice/css/32mp3/latest/bbcnewssummary.mp3")));
            _RadiosChannels.Add(new RadioChannel("RTL 2", new Uri("http://streaming.radio.rtl2.fr/rtl2-1-44-128")));
            _RadiosChannels.Add(new RadioChannel("RFM", new Uri("http://rfm-live-mp3-128.scdn.arkena.com/rfm.mp3")));
            _RadiosChannels.Add(new RadioChannel("RFM Night Fever", new Uri("http://audio.scdn.arkena.com/9134/lag_101525.mp3")));
            _RadiosChannels.Add(new RadioChannel("Jazz Radio Lounge", new Uri("http://broadcast.infomaniak.net:80/jazzlounge-high.mp3")));
            _RadiosChannels.Add(new RadioChannel("Jazz Radio Funk", new Uri("http://jazz-wr06.ice.infomaniak.ch/jazz-wr06-128.mp3")));
        }
    }
}
