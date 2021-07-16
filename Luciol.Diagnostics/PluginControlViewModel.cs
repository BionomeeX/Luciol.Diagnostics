using Avalonia.Threading;
using Luciol.Plugin;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace Luciol.Diagnostics
{
    public class PluginControlViewModel : APluginViewModel
    {
        public override void Init(APlugin plugin)
        {
            Plugin = plugin;
            plugin.Context.MainTriangle.OnDataLoading += (sender, e) =>
            {
                _triangleLoadTime = DateTime.Now;
            };
            plugin.Context.MainTriangle.OnDataLoaded += (sender, e) =>
            {
                _points.Add(DateTime.Now.Subtract(_triangleLoadTime).TotalMilliseconds);
                UpdatePerformanceGraph.Handle(_points.ToArray()).GetAwaiter().GetResult();
            };
            plugin.Context.MainTriangle.OnDataCleaned += (sender, e) =>
            {
                int count = _points.Count;
                Dispatcher.UIThread.Post(() => {
                    SetCleanPointGraph.Handle(count).GetAwaiter().GetResult();
                });
            };
        }

        public APlugin Plugin { set; get; }
        private DateTime _triangleLoadTime;
        private readonly List<double> _points = new();

        public Interaction<double[], Unit> UpdatePerformanceGraph { get; } = new();
        public Interaction<int, Unit> SetCleanPointGraph { get; } = new();
    }
}
