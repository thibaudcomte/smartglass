using System;
using System.Linq;
using System.Threading;

namespace SmartGlass.Clock
{
    public class ClockService : IClockService, IDisposable
    {
        /// <summary>
        /// Gets the main <see cref="ClockInfo"/>.
        /// </summary>
        public ClockInfo MainClockInfo { get; }

        /// <summary>
        /// Gets the auxiliary <see cref="ClockInfo"/>s.
        /// </summary>
        public ClockInfo[] AuxiliaryClocksInfo { get; }

        private readonly SynchronizationContext _SyncContext;


        /// <summary>
        /// Initializes the <see cref="ClockService"/>.
        /// </summary>
        /// <param name="settings"></param>
        public ClockService(IClockSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            _SyncContext = SynchronizationContext.Current;

            // set the main local clock
            MainClockInfo = new ClockInfo();

            // set the auxiliary clocks
            AuxiliaryClocksInfo = settings.AuxiliaryTimeZones.Select(aux => new ClockInfo(aux)).ToArray();

            // use a timer to run every 1 second
            _timer = new Timer(TimerCallback, null, 0, 1000);
        }

        /// <summary>
        /// Occurs when the clock ticks and a new second has ellapsed.
        /// </summary>
        public event EventHandler TimeChanged;

        /// <summary>
        /// Occurs when the local date has changed.
        /// </summary>
        public event EventHandler DateChanged;

        /// <summary>
        /// Releases all resources used by the <see cref="Timer"/> instance.
        /// </summary>
        public void Dispose()
        {
            TimeChanged = null;
            DateChanged = null;

            _timer?.Dispose();
        }

        #region internals

        private Timer _timer;
        private DateTime _oldDate;

        private void TimerCallback(object state = null)
        {
            try
            {
                _SyncContext.Post((o) =>
                {
                    // grab the current date and time so both events, if elligible,
                    // are raised with the same event args
                    var now = DateTime.Now;

                    // set the main clock
                    MainClockInfo.SetRawDateTimeNow(now);

                    // and the auxiliary clocks
                    if (AuxiliaryClocksInfo != null)
                    {
                        foreach (var aux in AuxiliaryClocksInfo)
                        {
                            aux.SetRawDateTimeNow(now);
                        }
                    }

                    // always raise the time changed event since this callback is called
                    // every 1 second and the clock service has a 1 second 'granularity'
                    TimeChanged?.Invoke(this, EventArgs.Empty);

                    // raise the date changed event if the day has changed from the old backup date
                    if (_oldDate == null || _oldDate.Day != now.Day)
                    {
                        DateChanged?.Invoke(this, EventArgs.Empty);
                        _oldDate = now;
                    }
                }, null);

            }
            catch
            {
                // silently ignore expections occuring in the event handlers.
            }
        }

        #endregion // internals
    }
}
