using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScottPlot.Avalonia;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace InterFace.Diagnostics
{
    public partial class PluginControl : ReactiveUserControl<PluginControlViewModel>
    {
        public PluginControl()
        {
            InitializeComponent();
            this.WhenActivated(_ =>
            {
                ViewModel.UpdatePerformanceGraph.RegisterHandler(UpdatePerformanceGraph);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private Task UpdatePerformanceGraph(InteractionContext<double[], Unit> interaction)
        {
            AvaPlot plot = this.FindControl<AvaPlot>("TrianglePerformancePlot");
            plot.Plot.AddScatter(Enumerable.Range(0, interaction.Input.Length).Select(x => (double)x).ToArray(), interaction.Input.ToArray());
            interaction.SetOutput(Unit.Default);

            return Task.CompletedTask;
        }
    }
}
