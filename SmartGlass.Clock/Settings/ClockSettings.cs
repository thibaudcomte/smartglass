using System;
using System.Linq;

namespace SmartGlass.Clock
{
    public class ClockSettings : IClockSettings
    {
        public TimeZoneInfo[] AuxiliaryTimeZones
        {
            get
            {
                // todo local/roaming settings

                var timeZoneIDs = string.Join(",", "Eastern Standard Time", "Central Pacific Standard Time");
                return timeZoneIDs.Split(',').Select(id => TimeZoneInfo.FindSystemTimeZoneById(id)).ToArray();
            }
        }

        public EClockTimeFormat ClockTimeFormat => throw new NotImplementedException();

        public EClockDateFormat ClockDateFormat => throw new NotImplementedException();
    }
}
