using Luciol.Plugin.Preference;
using System.Collections.ObjectModel;

namespace Luciol.Diagnostics
{
    public class PerformanceInfo
    {
        public PerformanceInfo(Plugin plugin)
        {
            _plugin = plugin;
        }

        /// <summary>
        /// Start performance timer
        /// </summary>
        public void StartTime()
        {
            _performanceTimer = DateTime.Now;
        }

        /// <summary>
        /// Stop performance timer and store the result
        /// </summary>
        public void CollectTimeInfo()
        {
            var delay = (float)DateTime.Now.Subtract(_performanceTimer).TotalMilliseconds;
            while (_data.Count >= ((NumberInputTextPreference<int>)_plugin.Preferences["nbPerformanceData"]).Value)
            {
                _data.RemoveAt(0);
            }
            _data.Add(delay);
        }

        /// <summary>
        /// Called when memory is collected
        /// </summary>
        public void AddMemoryCollection()
        {
            _memoryCollection.Add(_data.Count);
        }

        public ReadOnlyCollection<float> Data
        {
            get
            {
                return _data.AsReadOnly();
            }
        }

        public ReadOnlyCollection<int> MemoryCollection
        {
            get
            {
                return _memoryCollection.AsReadOnly();
            }
        }

        /// <summary>
        /// Contains all values of the performance (time in ms)
        /// </summary>
        private readonly List<float> _data = new();
        /// <summary>
        /// Performance timer, started when the main triangle begin to be loaded and "stopped" when it's done loading
        /// Allow to get information about the time it took to load data
        /// </summary>
        private DateTime _performanceTimer;
        /// <summary>
        /// Contains information about where Luciol memory collection passed
        /// Triangle data are loaded in the RAM so we don't need to read them from files everytimes
        /// But when too many are opened, we need to close some of them: that's what we call "memory collection"
        /// Values stored are the offset in _data List
        /// </summary>
        private readonly List<int> _memoryCollection = new();
        private readonly Plugin _plugin;
    }
}
