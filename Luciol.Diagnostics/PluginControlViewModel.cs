using Avalonia.Threading;
using Luciol.Plugin;
using ReactiveUI;
using System.Reactive;
using System.Windows.Input;

namespace Luciol.Diagnostics
{
    public class PluginControlViewModel : APluginViewModel
    {
        public PluginControlViewModel()
        {
            RefreshAll = ReactiveCommand.Create(() =>
            {
                var fisher = Plugin.GetDependency<Fisher_BoxPlot.Plugin>();
                var neo = Plugin.GetDependency<NEO.Plugin>();
                var geno = Plugin.GetDependency<GENO.Plugin>();

                if (fisher != null)
                {
                    fisher.LoadData();
                }
            });
        }

        public override void Init(ADisplayPlugin plugin)
        {
            Plugin = plugin;
            _performanceInfo = new(Plugin);
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
            /*
            plugin.Context.PositionTriangle.OnDataCleaned += (sender, e) =>
            {
                _performanceInfo.AddMemoryCollection();
                // Graph update must be done on UI thread but OnDataCleaned isn't called from there
                Dispatcher.UIThread.Post(() => {
                    UpdatePerformanceGraph.Handle(_performanceInfo).Subscribe(Observer.Create<Unit>(_ => { }));
                });
            };
            */
        }

        public ADisplayPlugin Plugin { private set; get; }
        private PerformanceInfo _performanceInfo;

        public Interaction<PerformanceInfo, Unit> UpdatePerformanceGraph { get; } = new();

        public ICommand RefreshAll { private set; get; }
    }
}
