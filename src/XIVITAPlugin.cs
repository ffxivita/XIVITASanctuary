namespace XIVITAGuide;

using Dalamud.IoC;
using Dalamud.Plugin;
using XIVITAGuide.Base;

internal class XIVITAPlugin : IDalamudPlugin
{
    /// <summary> 
    ///     The plugin name, fetched from PStrings.
    /// </summary>
    public string Name => PStrings.pluginName;

    /// <summary>
    ///     The plugin's main entry point.
    /// </summary>
    public XIVITAPlugin([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface)
    {
        pluginInterface.Create<PluginService>();
        PluginService.Initialize();

#if !DEBUG
        PluginService.ResourceManager.UpdateResources();
#endif
    }

    /// <summary>
    ///     Handles disposing of all resources used by the plugin.
    /// </summary>
    public void Dispose() => PluginService.Dispose();
}
