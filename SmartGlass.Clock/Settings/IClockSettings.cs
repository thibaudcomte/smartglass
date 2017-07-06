using System;

namespace SmartGlass.Clock
{
    /// <summary>
    /// Specifies the time format of a clock.
    /// </summary>
    public enum EClockTimeFormat
    {
        /// <summary>
        /// Short time pattern.
        /// https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#ShortTime
        /// </summary>
        Short,

        /// <summary>
        /// Long time pattern.
        /// https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#LongTime
        /// </summary>
        Long
    }

    /// <summary>
    /// Specifies the date format of a clock/calendar.
    /// </summary>
    public enum EClockDateFormat
    {
        /// <summary>
        /// Short date pattern.
        /// https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#ShortDate
        /// </summary>
        Short,

        /// <summary>
        /// Long date pattern.
        /// https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#LongDate
        /// </summary>
        Long
    }

    public interface IClockSettings
    {
        /// <summary>
        /// Gets the auxiliary time zones.
        /// </summary>
        TimeZoneInfo[] AuxiliaryTimeZones { get; }

        /// <summary>
        /// Gets the <see cref="EClockTimeFormat"/> value.
        /// </summary>
        EClockTimeFormat ClockTimeFormat { get; }

        /// <summary>
        /// Gets the <see cref="EClockDateFormat"/> value.
        /// </summary>
        EClockDateFormat ClockDateFormat { get; }
    }
}
