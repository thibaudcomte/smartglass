using SmartGlass.Radio.Models;
using System.Collections.Generic;

namespace SmartGlass.Radio.Settings
{
    public interface IRadioSettings
    {
        IEnumerable<RadioChannel> RadioChannels { get; }
    }
}
