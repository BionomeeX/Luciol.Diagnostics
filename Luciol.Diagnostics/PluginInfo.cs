using Luciol.Plugin;
using System;

namespace Luciol.Diagnostics
{
    public class PluginInfo : APluginInfo
    {
        public override string Description => "Diagnostic tools";

        public override string Version => "1.0.0";

        public override PluginType PluginType => PluginType.Display;

        protected override string Name => "Diagnostics";

        protected override string Author => "BionomeeX";

        protected override Type Plugin => typeof(Plugin);
    }
}
