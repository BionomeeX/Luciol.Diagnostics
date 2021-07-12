using Avalonia.Controls;
using InterFace.Plugin;
using InterFace.Plugin.Preference;
using System;
using System.Collections.Generic;

namespace InterFace.Diagnostics
{
    public class Plugin : APlugin
    {
        public override IEnumerable<IPreferenceExport> GetPreferences()
            => Array.Empty<IPreferenceExport>();

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
