using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Luciol.Diagnostics
{
    public class PerformanceInfo
    {
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
            _data.Add((float)DateTime.Now.Subtract(_performanceTimer).TotalMilliseconds);
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
    }
}
