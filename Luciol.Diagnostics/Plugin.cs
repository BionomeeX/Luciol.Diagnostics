using Avalonia.Controls;
using Luciol.Plugin;
using Luciol.Plugin.AssemblyAttribute;
using Luciol.Plugin.Preference;
using System.Collections.Generic;
using System.Reflection;

[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyAuthor("BionomeeX")]
[assembly: AssemblyDescription("Diagnostic tools")]

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
