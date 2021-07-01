using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace InterFace.Diagnostics
{
    public partial class PluginControl : ReactiveUserControl<PluginControlViewModel>
    {
        public PluginControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
