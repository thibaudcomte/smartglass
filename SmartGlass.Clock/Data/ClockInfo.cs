using Prism.Mvvm;
using System;

namespace SmartGlass.Clock
{
    public class ClockInfo : BindableBase
    {
        /// <summary>
        /// Gets the date and time.
        /// </summary>
        private DateTime _DateTimeNow;
        public DateTime DateTimeNow
        {
            get { return _DateTimeNow; }
            private set { SetProperty(ref _DateTimeNow, value); }
        }

        /// <summary>
        /// Gets the time zone.
        /// </summary>
        public TimeZoneInfo TimeZoneInfo { get; }

        /// <summary>
        /// Initializes a <see cref="ClockInfo"/>.
        /// </summary>
        public ClockInfo() : this(null)
        {
        }

        /// <summary>
        /// Initializes a <see cref="ClockInfo"/> with a given <see cref="System.TimeZoneInfo"/>.
        /// </summary>
        /// <param name="zoneInfo">the time zone info. if null, we default to the local zone.</param>
        public ClockInfo(TimeZoneInfo zoneInfo = null)
        {
            TimeZoneInfo = zoneInfo ?? TimeZoneInfo.Local;
            _DateTimeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo);
        }

        internal void SetRawDateTimeNow(DateTime dt)
        {
            DateTimeNow = TimeZoneInfo.ConvertTime(dt, TimeZoneInfo);
        }
    }
}
