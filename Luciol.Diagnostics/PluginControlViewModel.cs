using Avalonia.Threading;
using Luciol.Plugin;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace Luciol.Diagnostics
{
    public class PluginControlViewModel : APluginViewModel
    {
        public override void Init(ADisplayPlugin plugin)
        {
            Plugin = plugin;
            plugin.Context.MainTriangle.OnDataLoading += (sender, e) =>
            {
                _performanceInfo.StartTime();
            };
            plugin.Context.MainTriangle.OnDataLoaded += (sender, e) =>
            {
                _performanceInfo.CollectTimeInfo();
                UpdatePerformanceGraph.Handle(_performanceInfo).GetAwaiter().GetResult();
            };
            plugin.Context.MainTriangle.OnDataCleaned += (sender, e) =>
            {
                _performanceInfo.AddMemoryCollection();
                // Graph update must be done on UI thread but OnDataCleaned isn't called from there
                Dispatcher.UIThread.Post(() => {
                    UpdatePerformanceGraph.Handle(_performanceInfo).GetAwaiter().GetResult();
                });
            };
        }

        public ADisplayPlugin Plugin { private set; get; }
        private readonly PerformanceInfo _performanceInfo = new();

        public Interaction<PerformanceInfo, Unit> UpdatePerformanceGraph { get; } = new();
    }
}
