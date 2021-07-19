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
            AttachedToLogicalTree += (sender, e) =>
            {
                ViewModel.UpdatePerformanceGraph.RegisterHandler(UpdatePerformanceGraph);
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private Task UpdatePerformanceGraph(InteractionContext<(double[], int[]), Unit> interaction)
        {
            AvaPlot plot = this.FindControl<AvaPlot>("TrianglePerformancePlot");
            plot.Plot.Clear();
            plot.Plot.AddScatter(
                xs: Enumerable.Range(0, interaction.Input.Item1.Length).Select(x => (double)x).ToArray(),
                ys: interaction.Input.Item1.ToArray(),
                color: ((Color)ViewModel.Plugin.Preferences["performanceMainColor"].Value).ToSystemColor()
            );

            foreach (var point in interaction.Input.Item2)
            {
                plot.Plot.AddVerticalLine(point, color: ((Color)ViewModel.Plugin.Preferences["performanceMemoryMarkColor"].Value).ToSystemColor());
            }

            interaction.SetOutput(Unit.Default);

            return Task.CompletedTask;
        }
    }
}
