using System;
using Dalamud.Game.Command;
using Dalamud.Logging;
using XIVITAGuide.Base;
using XIVITAGuide.Localization;
using XIVITAGuide.UI.Windows.Editor;
using XIVITAGuide.UI.Windows.GuideViewer;
using XIVITAGuide.UI.Windows.GuideList;
using XIVITAGuide.UI.Windows.Settings;

namespace XIVITAGuide.Managers
{
    /// <summary>
    ///     Initializes and manages all commands and command-events for the plugin.
    /// </summary>
    public sealed class CommandManager : IDisposable
    {
        private const string ListCommand = "/xivita-list";
        private const string SettingsCommand = "/xivita-config";
        private const string EditorCommand = "/xivita-editor";
        private const string GuideViewerCommand = "/xivita-guide";

        /// <summary>
        ///     Initializes the CommandManager and its resources.
        /// </summary>
        public CommandManager()
        {
            PluginLog.Debug("CommandManager(Constructor): Initializing...");

            PluginService.Commands.AddHandler(ListCommand, new CommandInfo(this.OnCommand) { HelpMessage = TCommands.GuideListHelp });
            PluginService.Commands.AddHandler(SettingsCommand, new CommandInfo(this.OnCommand) { HelpMessage = TCommands.SettingsHelp });
            PluginService.Commands.AddHandler(EditorCommand, new CommandInfo(this.OnCommand) { HelpMessage = TCommands.EditorHelp });
            PluginService.Commands.AddHandler(GuideViewerCommand, new CommandInfo(this.OnCommand) { HelpMessage = TCommands.InfoHelp });

            PluginLog.Debug("CommandManager(Constructor): Initialization complete.");
        }

        /// <summary>
        ///     Dispose of the PluginCommandManager and its resources.
        /// </summary>
        public void Dispose()
        {
            PluginService.Commands.RemoveHandler(ListCommand);
            PluginService.Commands.RemoveHandler(SettingsCommand);
            PluginService.Commands.RemoveHandler(EditorCommand);
            PluginService.Commands.RemoveHandler(GuideViewerCommand);

            PluginLog.Debug("CommandManager(Dispose): Successfully disposed.");
        }

        /// <summary>
        ///     Event handler for when a command is issued by the user.
        /// </summary>
        private void OnCommand(string command, string args)
        {
            var windowSystem = PluginService.WindowManager.WindowSystem;
            switch (command)
            {
                case ListCommand:
                    if (windowSystem.GetWindow(TWindowNames.GuideList) is GuideListWindow guideListWindow)
                    {
                        guideListWindow.IsOpen = !guideListWindow.IsOpen;
                    }

                    break;
                case SettingsCommand:
                    if (windowSystem.GetWindow(TWindowNames.Settings) is SettingsWindow settingsWindow)
                    {
                        settingsWindow.IsOpen = !settingsWindow.IsOpen;
                    }

                    break;
                case EditorCommand:
                    if (windowSystem.GetWindow(TWindowNames.GuideEditor) is EditorWindow editorWindow)
                    {
                        editorWindow.IsOpen = !editorWindow.IsOpen;
                    }

                    break;
                case GuideViewerCommand:
                    if (windowSystem.GetWindow(TWindowNames.GuideViewer) is GuideViewerWindow guideViewerScreen)
                    {
                        guideViewerScreen.IsOpen = !guideViewerScreen.IsOpen;
                    }
                    break;
            }
        }
    }
}
