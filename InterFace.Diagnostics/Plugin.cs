using Avalonia.Controls;
using InterFace.Plugin;
using InterFace.Plugin.Preference;
using System.Collections.Generic;

namespace InterFace.Diagnostics
{
    public class Plugin : APlugin
    {
        public override IEnumerable<IPreferenceExport> GetPreferences()
            => new IPreferenceExport[]
            {
                PerformanceGraphColor, PerformanceMemoryMarkColor
            };

        public ColorPreference PerformanceGraphColor { get; } = new("performanceMainColor", "Performance Graph Color", Color.Blue);
        public ColorPreference PerformanceMemoryMarkColor { get; } = new("performanceMemoryMarkColor", "Performance Memory Annotation Color", Color.Red);

        public override Control GetView()
            => new PluginControl();

        public override object GetViewModel()
            => _viewModel;

        protected override void Init()
        {
            _viewModel.Init(this, Context);
        }

        private readonly PluginControlViewModel _viewModel = new();
    }
}
