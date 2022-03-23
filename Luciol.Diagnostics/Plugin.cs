using Luciol.Plugin.Core;
using Luciol.Plugin.Preference;

namespace Luciol.Diagnostics
{
    public class Plugin : ADisplayPlugin
    {
        protected override IEnumerable<IPreferenceExport> GetPreferences()
            => new IPreferenceExport[]
            {
                new ColorPreference("performanceMemoryMarkColor", "Performance memory annotation color", Color.Red),
                new NumberInputTextPreference<int>("nbPerformanceData", "Number of data displayed by performance graph", 200)
            };

        protected override IPluginView GetView()
            => new PluginControl();

        protected override APluginViewModel GetViewModel()
            => new PluginControlViewModel();
    }
}
