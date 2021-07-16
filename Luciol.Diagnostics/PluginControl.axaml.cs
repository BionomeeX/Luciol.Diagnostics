using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Luciol.Plugin.Preference;
using ReactiveUI;
using ScottPlot.Avalonia;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Luciol.Diagnostics
{
    public class PluginControl : ReactiveUserControl<PluginControlViewModel>
    {
        public PluginControl()
        {
            InitializeComponent();
            this.WhenActivated(_ =>
            {
                if (!_isInit)
                {
                    ViewModel.UpdatePerformanceGraph.RegisterHandler(UpdatePerformanceGraph);
                    ViewModel.SetCleanPointGraph.RegisterHandler(SetCleanPointGraph);
                    _isInit = true; // TODO: Can probably be handle by APlugin
                }
            });
        }

        private bool _isInit;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private Task UpdatePerformanceGraph(InteractionContext<double[], Unit> interaction)
        {
            AvaPlot plot = this.FindControl<AvaPlot>("TrianglePerformancePlot");
            plot.Plot.AddScatter(
                xs: Enumerable.Range(0, interaction.Input.Length).Select(x => (double)x).ToArray(),
                ys: interaction.Input.ToArray(),
                color: ((Color)ViewModel.Plugin.Preferences["performanceMainColor"].Value).ToSystemColor()
            );
            interaction.SetOutput(Unit.Default);

            return Task.CompletedTask;
        }

        private Task SetCleanPointGraph(InteractionContext<int, Unit> interaction)
        {
            AvaPlot plot = this.FindControl<AvaPlot>("TrianglePerformancePlot");
            plot.Plot.AddVerticalLine(interaction.Input, color: ((Color)ViewModel.Plugin.Preferences["performanceMemoryMarkColor"].Value).ToSystemColor());
            interaction.SetOutput(Unit.Default);

            return Task.CompletedTask;
        }
    }
}
