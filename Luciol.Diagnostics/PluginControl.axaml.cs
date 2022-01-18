using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Hparg;
using Luciol.Plugin;
using Luciol.Plugin.Preference;
using ReactiveUI;
using System.Linq;
using System.Reactive;

namespace Luciol.Diagnostics
{
    public class PluginControl : APluginView<PluginControlViewModel>
    {
        public PluginControl()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void Init(ADisplayPlugin plugin)
        {
            ViewModel.UpdatePerformanceGraph.RegisterHandler(UpdatePerformanceGraph);
        }

        private void UpdatePerformanceGraph(InteractionContext<PerformanceInfo, Unit> interaction)
        {
            // Get performance plot on the view
            var plot = this.FindControl<Graph>("TrianglePerformancePlot");

            // Create a new scatter plot with:
            // On X axis: The index of all our values
            // On Y axis: All the values to display
            // Color: The one set by the user in the global preferences
            plot.Plot = new Scatter(
                x: Enumerable.Range(0, interaction.Input.Data.Count).Select(x => (float)x).ToArray(),
                y: interaction.Input.Data.ToArray(),
                color: ViewModel.Plugin.Context.GlobalSettings.Graph.MainColor.Value
            );

            // For all memory collection that happened, we display a vertical line there on the graph of the color defined in the plugin preferences
            foreach (var point in interaction.Input.MemoryCollection)
            {
                plot.Plot.AddVerticalLine(point, color: ((Color)ViewModel.Plugin.Preferences["performanceMemoryMarkColor"].ObjValue).ToSystemColor());
            }

            interaction.SetOutput(Unit.Default);
        }
    }
}
