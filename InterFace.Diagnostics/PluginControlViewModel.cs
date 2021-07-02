using InterFace.Plugin;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace InterFace.Diagnostics
{
    public class PluginControlViewModel : ReactiveObject
    {
        public void Init(IContext context)
        {
            context.MainTriangle.OnDataLoading += (sender, e) =>
            {
                _triangleLoadTime = DateTime.Now;
            };
            context.MainTriangle.OnDataLoaded += (sender, e) =>
            {
                _points.Add(DateTime.Now.Subtract(_triangleLoadTime).TotalMilliseconds);
                UpdatePerformanceGraph.Handle(_points.ToArray()).GetAwaiter().GetResult();
            };
        }

        private DateTime _triangleLoadTime;
        private readonly List<double> _points = new();

        public Interaction<double[], Unit> UpdatePerformanceGraph { get; } = new();
    }
}
