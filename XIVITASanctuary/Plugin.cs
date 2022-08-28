namespace XIVITASanctuary
{
    using System.IO;
    using Dalamud.Data;
    using Dalamud.Game;
    using Dalamud.Game.Command;
    using Dalamud.IoC;
    using Dalamud.Plugin;
    using Dalamud.Interface.Windowing;

    public sealed class Plugin : IDalamudPlugin {
        public string Name => "ReSanctuary";
        private const string CommandName = "/sanctuary";

        [PluginService] public static DalamudPluginInterface PluginInterface { get; private set; }
        [PluginService] public static CommandManager CommandManager { get; private set; }
        [PluginService] public static DataManager DataManager { get; private set; }
        [PluginService] public static SigScanner SigScanner { get; private set; }

        public Configuration Configuration { get; private set; }
        public WindowSystem WindowSystem = new("XIVITASanctuary");

        public Plugin() {
            Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            Configuration.Initialize(PluginInterface);

            WindowSystem.AddWindow(new ConfigWindow(this));
            WindowSystem.AddWindow(new MainWindow(this));
            WindowSystem.AddWindow(new WidgetWindow(this));

            CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand) {
                HelpMessage = "Apre l'interfaccia principale di XIVITASanctuary."
            });

            PluginInterface.UiBuilder.Draw += DrawUI;
            PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
        }

        public void Dispose() {
            WindowSystem.RemoveAllWindows();
            CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args) {
            switch (args) {
                case "settings":
                case "config":
                    DrawConfigUI();
                    break;
                case "widget":
                    WindowSystem.GetWindow("XIVITASanctuary Widget").IsOpen = true;
                    break;
                default:
                    WindowSystem.GetWindow("XIVITASanctuary").IsOpen = true;
                    break;
            }
        }

        private void DrawUI() {
            WindowSystem.Draw();
        }

        public void DrawConfigUI() {
            WindowSystem.GetWindow("XIVITASanctuary Config").IsOpen = true;
        }
    }
}