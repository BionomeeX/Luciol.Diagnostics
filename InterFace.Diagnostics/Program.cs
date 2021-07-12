using Avalonia;

namespace InterFace.Diagnostics
{
    static class Program
    {
        public static void Main()
        {
            Plugin myPlugin = new();
            myPlugin.Test<PluginControl, PluginControlViewModel>();
        }

        public static AppBuilder BuildAvaloniaApp()
            => InterFace.Plugin.APlugin.BuildAvaloniaApp<PluginControl, PluginControlViewModel>();
    }
}
