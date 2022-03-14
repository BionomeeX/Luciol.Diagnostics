using Luciol.Plugin;

namespace Luciol.Diagnostics
{
    public class PluginInfo : APluginInfo
    {
        public override string Description => "Diagnostic tools";

        public override string Version => "1.0.0";

        public override PluginType PluginType => PluginType.Display;

        public override string Name => "Diagnostics";

        public override string Author => "BionomeeX";

        protected override Type Plugin => typeof(Plugin);
        public override Dependency[] Dependencies => new[]
        {
            new Dependency(typeof(Fisher_BoxPlot.PluginInfo), isMandatory: false),
            new Dependency(typeof(NEO.PluginInfo), isMandatory: false),
            new Dependency(typeof(GENO.PluginInfo), isMandatory: false)
        };
    }
}
