using Avalonia.Controls;
using Luciol.Plugin;
using Luciol.Plugin.Preference;
using System.Collections.Generic;

namespace Luciol.Diagnostics
{
    public class Plugin : ADisplayPlugin
    {
        protected override IEnumerable<IPreferenceExport> GetPreferences()
            => new IPreferenceExport[]
            {
                new ColorPreference("performanceMemoryMarkColor", "Performance Memory Annotation Color", Color.Red)
            };

        protected override Control GetView()
            => new PluginControl();

        protected override APluginViewModel GetViewModel()
            => new PluginControlViewModel();
    }
}
