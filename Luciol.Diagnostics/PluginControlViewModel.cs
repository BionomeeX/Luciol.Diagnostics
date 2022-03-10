using Avalonia.Threading;
using Luciol.Plugin;
using ReactiveUI;
using System.Reactive;

namespace Luciol.Diagnostics
{
    public class PluginControlViewModel : APluginViewModel
    {
        public override void Init(ADisplayPlugin plugin)
        {
            Plugin = plugin;
            plugin.Context.PositionTriangle.OnDataLoading += (sender, e) =>
            {
                _performanceInfo.StartTime();
            };
            plugin.Context.PositionTriangle.OnDataLoaded += (sender, e) =>
            {
                _performanceInfo.CollectTimeInfo();
                Dispatcher.UIThread.Post(() => {
                    UpdatePerformanceGraph.Handle(_performanceInfo).Subscribe(Observer.Create<Unit>(_ => { }));
                });
            };
            plugin.Context.PositionTriangle.OnDataCleaned += (sender, e) =>
            {
                _performanceInfo.AddMemoryCollection();
                // Graph update must be done on UI thread but OnDataCleaned isn't called from there
                Dispatcher.UIThread.Post(() => {
                    UpdatePerformanceGraph.Handle(_performanceInfo).Subscribe(Observer.Create<Unit>(_ => { }));
                });
            };
        }

        public ADisplayPlugin Plugin { private set; get; }
        private readonly PerformanceInfo _performanceInfo = new();

        public Interaction<PerformanceInfo, Unit> UpdatePerformanceGraph { get; } = new();
    }
}
