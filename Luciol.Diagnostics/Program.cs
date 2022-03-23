using Avalonia;

namespace Luciol.Diagnostics
{
    static class Program
    {
        public static void Main()
        {
            Plugin myPlugin = new();
            myPlugin.Test<PluginControl, PluginControlViewModel>();
        }

        public static AppBuilder BuildAvaloniaApp()
            => Luciol.Plugin.Core.ADisplayPlugin.BuildAvaloniaApp<PluginControl, PluginControlViewModel>();
    }
}
