using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers
{

    public delegate void TimerDatabaseAddingTimerDelegate(Timer timer);
    public delegate void TimerDatabaseAddedTimerDelegate(Timer timer);

    public delegate void TimerDatabaseRemovingTimerDelegate(Timer timer);
    public delegate void TimerDatabaseRemovedTimerDelegate(Timer timer);

    public delegate void TimerDatabaseElementsChangingDelegate();
    public delegate void TimerDatabaseElementsChangedDelegate();

    class TimerDatabase
    {

        public static TimerDatabase Instance { get; } = new TimerDatabase();

        private Dictionary<int, Timer> timers = new Dictionary<int, Timer>();

        public event TimerDatabaseElementsChangingDelegate ElementsChanging;
        public event TimerDatabaseElementsChangedDelegate ElementsChanged;

        public event TimerDatabaseAddingTimerDelegate AddingTimer;
        public event TimerDatabaseAddedTimerDelegate AddedTimer;

        public event TimerDatabaseRemovingTimerDelegate RemovingTimer;
        public event TimerDatabaseRemovedTimerDelegate RemovedTimer;

        public IReadOnlyDictionary<int, Timer> TimersWithKeys
        {
            get => timers;
        }

        public IReadOnlyList<Timer> Timers
        {
            get => timers.Values.ToList();
        }

        public void Add(Timer timer)
        {

            // Validate argument
            if (timer == null)
                throw new ArgumentNullException();

            // Validate ID
            int id = timer.ID;
            if (id <= 0)
                throw new Exception();

            if (timers.ContainsKey(id))
                throw new Exception();

            // Add element
            ElementsChanging?.Invoke();
            AddingTimer?.Invoke(timer);

            timers.Add(timer.ID, timer);

            AddedTimer?.Invoke(timer);
            ElementsChanged?.Invoke();

        }

        public bool Remove(Timer timer)
        {

            // Validate argument
            if (timer == null)
                throw new ArgumentNullException();

            if (!timers.ContainsValue(timer))
                return false;

            // Remove element
            ElementsChanging?.Invoke();
            RemovingTimer?.Invoke(timer);

            timers.Remove(timer.ID);

            RemovedTimer?.Invoke(timer);
            ElementsChanged?.Invoke();

            return true;

        }

    }
}
