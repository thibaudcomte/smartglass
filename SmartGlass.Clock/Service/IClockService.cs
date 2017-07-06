using System;

namespace SmartGlass.Clock
{
    public interface IClockService
    {
        /// <summary>
        /// Gets the current date and time at the current location.
        /// This is the main clock.
        /// </summary>
        ClockInfo MainClockInfo { get; }

        /// <summary>
        /// Gets the auxiliary clocks.
        /// </summary>
        ClockInfo[] AuxiliaryClocksInfo { get; }

        /// <summary>
        /// Occurs when the clock ticks and a new second has ellapsed.
        /// </summary>
        event EventHandler TimeChanged;

        /// <summary>
        /// Occurs when the date has changed.
        /// </summary>
        event EventHandler DateChanged;
    }
}
