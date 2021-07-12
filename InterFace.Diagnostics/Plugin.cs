using Avalonia.Controls;
using Avalonia.Media;
using InterFace.Plugin;
using InterFace.Plugin.Preference;
using System;
using System.Collections.Generic;

namespace InterFace.Diagnostics
{
    public class Plugin : APlugin
    {
        public override IEnumerable<IPreferenceExport> GetPreferences()
            => new IPreferenceExport[]
            {
                new ColorPreference("performanceMainColor", "Performance Graph Color", Color.FromRgb(0, 0, 255)),
                new ColorPreference("performanceMemoryMarkColor", "Performance Memory Annotation Color", Color.FromRgb(255, 0, 0))
            };

        public override Control GetView()
            => new PluginControl();

        public override object GetViewModel()
            => _viewModel;

        protected override void Init()
        {
            _viewModel.Init(Context);
        }

        private readonly PluginControlViewModel _viewModel = new();
    }
}
