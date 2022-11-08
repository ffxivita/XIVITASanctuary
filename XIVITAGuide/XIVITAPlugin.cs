using Dalamud.IoC;
using Dalamud.Plugin;
using XIVITAGuide.Base;

namespace XIVITAGuide
{
    internal sealed class XIVITAPlugin : IDalamudPlugin
    {

        /// <summary>
        ///     The plugin name, fetched from PluginConstants.
        /// </summary>
        public string Name => PluginConstants.PluginName;

        /// <summary>
        ///     The plugin's main entry point.
        /// </summary>
        public XIVITAPlugin([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface)
        {
            pluginInterface.Create<PluginService>();
            PluginService.Initialize();
        }
        public void Dispose() => PluginService.Dispose();
    }
}