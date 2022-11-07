namespace XIVITAGuide.UI.Screens.Settings;

using System;
using System.IO;
using CheapLoc;
using Dalamud.Interface.ImGuiFileDialog;
using Dalamud.Interface.Internal.Notifications;
using XIVITAGuide.Base;

sealed public class SettingsPresenter : IDisposable
{
    public SettingsPresenter() { }
    public void Dispose() { }

    public bool isVisible = false;

#if DEBUG
    public FileDialogManager dialogManager = new FileDialogManager();

    /// <summary>
    ///     Handles the directory select event and saves the location to that directory.
    /// </summary>
    public void OnDirectoryPicked(bool success, string path)
    {
        if (!success) return;
        var directory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(path);
        Loc.ExportLocalizable();
        File.Copy(Path.Combine(path, "XIVITAGuide_Localizable.json"), Path.Combine(path, "it.json"), true);
        Directory.SetCurrentDirectory(directory);
        PluginService.PluginInterface.UiBuilder.AddNotification("Localization exported successfully.", PStrings.pluginName, NotificationType.Success);
    }
#endif
}
