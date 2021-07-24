using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Hparg;
using Luciol.Plugin.Preference;
using ReactiveUI;
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
            var plot = this.FindControl<Manhattan>("TrianglePerformancePlot");
            plot.Plot.Clear();
            plot.Plot.AddPoints(
                x: Enumerable.Range(0, interaction.Input.Item1.Length).Select(x => (float)x).ToArray(),
                y: interaction.Input.Item1.Select(x => (float)x).ToArray(),
                color: ((Color)ViewModel.Plugin.Preferences["performanceMainColor"].Value).ToSystemColor()
            );

            foreach (var point in interaction.Input.Item2)
            {
                //plot.Plot.AddVerticalLine(point, color: ((Color)ViewModel.Plugin.Preferences["performanceMemoryMarkColor"].Value).ToSystemColor());
            }

            interaction.SetOutput(Unit.Default);

            return Task.CompletedTask;
        }
    }
}
