using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Style;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using ImGuiScene;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace XIVITASanctuary.Windows
{
    public class WidgetWindow : Window, IDisposable {
    private Plugin Plugin;

    
    private RawExcelSheet itemPouchSheet;
    private ExcelSheet<Item> itemSheet;

    private Dictionary<uint, TextureWrap> todoTextureCache;

    public WidgetWindow(Plugin plugin) : base("XIVITASanctuary Widget") {
        SizeConstraints = new WindowSizeConstraints {
            MinimumSize = new Vector2(150, 75) * ImGuiHelpers.GlobalScale,
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        Plugin = plugin;

        itemPouchSheet = Plugin.DataManager.Excel.GetSheetRaw("MJIItemPouch");
        itemSheet = Plugin.DataManager.Excel.GetSheet<Item>();

        todoTextureCache = new();
    }

    public void Dispose() { }

    public override void PreDraw() {
        if (Plugin.Configuration.LockWidget) {
            Flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoMove |
                    ImGuiWindowFlags.NoTitleBar;
            RespectCloseHotkey = false;
        } else {
            Flags = ImGuiWindowFlags.None;
            RespectCloseHotkey = true;
        }
    }

    private void DrawGarbage(uint id, int amount, Item item, int has) {
        var iconSize = ImGui.GetTextLineHeight() * 1.25f;
        var iconSizeVec = new Vector2(iconSize, iconSize);
        ImGui.Image(Utils.IconCache(item.Icon).ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

        ImGui.SameLine();

        if (has >= amount) ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.HealerGreen);
        if (ImGui.Selectable($"{item.Name} - {has}/{amount}")) {
            var mat = Utils.GetSortedGatheringItems().Find(x => x.ItemID == item.RowId);
            if (mat != null) {
                var territoryTypeSheet = Plugin.DataManager.Excel.GetSheet<TerritoryType>();
                var islandSanctuary = territoryTypeSheet.First(x => x.Name == "h1m2");
                var teri = islandSanctuary.RowId;

                Utils.OpenGatheringMarker(teri, mat.X, mat.Y, mat.Radius, mat.Name);
            }
        }

        if (has >= amount) ImGui.PopStyleColor();
    }

    public override void Draw() {
        var todoList = Plugin.Configuration.TodoList;

        // sono al secondo giorno di ferie e sono già esausto. Ecco il codice ottimizzato\
        foreach (var (id, amount) in todoList) {
            var itemPouchItem = itemPouchSheet.GetRow(id);
            var item = itemSheet.GetRow(itemPouchItem.ReadColumn<uint>(0));
            var has = Utils.GetStackSize(item.RowId);

            if (has < amount) DrawGarbage(id, amount, item, has);
        }

        foreach (var (id, amount) in todoList) {
            var itemPouchItem = itemPouchSheet.GetRow(id);
            var item = itemSheet.GetRow(itemPouchItem.ReadColumn<uint>(0));
            var has = Utils.GetStackSize(item.RowId);

            if (has >= amount) DrawGarbage(id, amount, item, has);
        }
    }
}
}