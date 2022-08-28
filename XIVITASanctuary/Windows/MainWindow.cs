namespace XIVITASanctuary
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Numerics;
    using Dalamud.Interface;
    using Dalamud.Interface.Windowing;
    using Dalamud.Logging;
    using ImGuiNET;
    using ImGuiScene;
    using Lumina.Excel;
    using Lumina.Excel.GeneratedSheets;

    public class MainWindow : Window, IDisposable {
    private Plugin Plugin;

    private List<GatheringItem> gatheringItems;
    private List<WorkshopItem> workshopItems;

    private string gatheringSearchFilter = string.Empty;
    private string workshopSearchFilter = string.Empty;
    private int workshopSearchSelected;

    private ExcelSheet<TerritoryType> territoryTypeSheet;
    private ExcelSheet<MJIItemPouch> itemPouchSheet;

    private Dictionary<uint, TextureWrap> todoTextureCache;

    public MainWindow(Plugin plugin) : base("XIVITASanctuary") {
        SizeConstraints = new WindowSizeConstraints {
            MinimumSize = new Vector2(300, 300) * ImGuiHelpers.GlobalScale,
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
        Size = new Vector2(600, 400);
        SizeCondition = ImGuiCond.FirstUseEver;

        Plugin = plugin;
        gatheringItems = Utils.GetSortedGatheringItems();
        workshopItems = Utils.GetSortedWorkshopItems();

        territoryTypeSheet = Plugin.DataManager.Excel.GetSheet<TerritoryType>();
        itemPouchSheet = Plugin.DataManager.Excel.GetSheet<MJIItemPouch>();
        
        todoTextureCache = new();
    }

    public void Dispose() { }

    private void DrawGatheringTab() {
        var tableFlags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit;

        ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X);
        ImGui.InputText(string.Empty, ref gatheringSearchFilter, 256);

        if (ImGui.BeginTable("XIVITASanctuary_MainWindowTable", 4, tableFlags)) {
            ImGui.TableSetupColumn("Icona");
            ImGui.TableSetupColumn("Nome");
            ImGui.TableSetupColumn("Bottoni");
            ImGui.TableSetupColumn("Tool Richiesto");
            ImGui.TableHeadersRow();

            foreach (var item in gatheringItems) {
                if (!item.Name.ToLower().Contains(gatheringSearchFilter.ToLower())) continue;

                ImGui.TableNextRow();

                ImGui.TableSetColumnIndex(0);
                var icon = item.Icon;
                var iconSize = ImGui.GetTextLineHeight() * 1.5f;
                var iconSizeVec = new Vector2(iconSize, iconSize);
                ImGui.Image(icon.ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

                ImGui.TableSetColumnIndex(1);
                ImGui.Text(item.Name);

                ImGui.TableSetColumnIndex(2);
                if (ImGui.Button("Mostra in mappa##XIVITASanctuary_ShowOnMap_" + item.ItemID)) {
                    var islandSanctuary = territoryTypeSheet.First(x => x.Name == "h1m2");
                    var teri = islandSanctuary.RowId;

                    PluginLog.Debug("radius: {radius}", item.Radius);

                    Utils.OpenGatheringMarker(teri, item.X, item.Y, item.Radius, item.Name);
                }
                
                ImGui.SameLine();

                if (ImGui.Button("Aggiungi alla lista ToDo##XIVITASanctuary_GatheringAddTodo_" + item.ItemID)) {
                    var rowID = itemPouchSheet.First(x => {
                        var itemValue = x.Item.Value;
                        if (itemValue == null) return false;
                        return itemValue.RowId == item.ItemID;
                    }).RowId;
                    Utils.AddToTodoList(Plugin.Configuration, rowID);
                }

                ImGui.TableSetColumnIndex(3);
                ImGui.Text(item.RequiredTool != null ? item.RequiredTool.Name : "None");
            }

            ImGui.EndTable();
        }
    }

    private void DrawWorkshopTab() {
        var contentRegionAvail = ImGui.GetContentRegionAvail();

        {
            var imguiSucks = ImGui.CalcTextSize("..").X;
            var childSize = contentRegionAvail with { X = imguiSucks };
            ImGui.BeginChild("XIVITASanctuary_WorkshopListSearchChild", childSize);

            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X);
            ImGui.InputText(string.Empty, ref workshopSearchFilter, 256);

            if (ImGui.BeginListBox("##XIVITASanctuary_WorkshopList", ImGui.GetContentRegionAvail())) {
                for (var i = 0; i < workshopItems.Count; i++) {
                    var item = workshopItems[i];

                    if (item.Name.ToLower().Contains(workshopSearchFilter.ToLower())) {
                        var selected = i == workshopSearchSelected;

                        if (ImGui.Selectable(item.Name, selected)) workshopSearchSelected = i;
                        if (selected) ImGui.SetItemDefaultFocus();
                    }
                }

                ImGui.EndListBox();
            }

            ImGui.EndChild();
        }

        ImGui.SameLine();

        {
            ImGui.BeginChild("XIVITASanctuary_WorkshopListViewChild");

            var item = workshopItems[workshopSearchSelected];

            var icon = item.Icon;
            var iconSize = ImGui.GetTextLineHeight() * 2f;
            var iconSizeVec = new Vector2(iconSize, iconSize);
            ImGui.Image(icon.ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

            ImGui.SameLine();

            ImGui.Text($"{item.Name}\nDurata: {item.CraftingTime} ore");
            
            if (ImGui.Button("Aggiungi alla lista ToDO##XIVITASanctuary_WorkshopAddTodo_" + item.ItemID)) {
                foreach (var (requiredMat, matCount) in item.Materials) {
                    Utils.AddToTodoList(Plugin.Configuration, requiredMat, matCount);
                }
                
                Plugin.WindowSystem.GetWindow("XIVITASanctuary Widget").IsOpen = true;
            }

            ImGui.Text("Materiali:");
            foreach (var (requiredMat, matCount) in item.Materials) {
                var itemPouchRow = itemPouchSheet.GetRow(requiredMat);
                var itemPouchItem = itemPouchRow.Item.Value;
                
                var mat = gatheringItems.Find(x => x.ItemID == itemPouchItem.RowId);

                var name = itemPouchItem.Name;
                var text = $"{name} x{matCount}";

                if (ImGui.TreeNode(text)) {
                    var matIconSize = ImGui.GetTextLineHeight() * 2;
                    var matIconSizeVec = new Vector2(matIconSize, matIconSize);

                    if (mat != null) {
                        ImGui.Image(mat.Icon.ImGuiHandle, matIconSizeVec, Vector2.Zero, Vector2.One);
                        ImGui.SameLine();
                        ImGui.Text($"Tool richiesto: {(mat.RequiredTool != null ? mat.RequiredTool.Name : "Nessuno")}");

                        if (ImGui.Button("Mostra in mappa##XIVITASanctuary_WorkshopShowOnMap_" + mat.ItemID)) {
                            var islandSanctuary = territoryTypeSheet.First(x => x.Name == "h1m2");
                            var teri = islandSanctuary.RowId;

                            Utils.OpenGatheringMarker(teri, mat.X, mat.Y, mat.Radius, mat.Name);
                            Utils.OpenGatheringMarker(teri, mat.X, mat.Y, mat.Radius, mat.Name);
                        }
                    } else {
                        var texture = Plugin.DataManager.GetImGuiTextureIcon(itemPouchItem.Icon);
                        ImGui.Image(texture.ImGuiHandle, matIconSizeVec, Vector2.Zero, Vector2.One);
                        ImGui.SameLine();
                        ImGui.Text("No data available :(");
                    }

                    ImGui.TreePop();
                }
            }
            
            ImGui.EndChild();
        }
    }

    private void DrawTodoTab() {
        var todoList = Plugin.Configuration.TodoList;

        if (ImGui.Button("Apri Widget ToDo")) {
            Plugin.WindowSystem.GetWindow("XIVITASanctuary Widget").IsOpen = true;
        }

        foreach (var (id, amount) in todoList) {
            var item = itemPouchSheet.GetRow(id).Item.Value;
            var amnt = amount;

            TextureWrap? icon;
            if (todoTextureCache.ContainsKey(id)) {
               icon = todoTextureCache[id]; 
            } else {
                icon = Plugin.DataManager.GetImGuiTextureIcon(item.Icon);
                todoTextureCache[id] = icon;
            }
            
            var iconSize = ImGui.GetTextLineHeight() * 1.25f;
            var iconSizeVec = new Vector2(iconSize, iconSize);
            ImGui.Image(icon.ImGuiHandle, iconSizeVec, Vector2.Zero, Vector2.One);

            ImGui.PushItemWidth(100 * ImGuiHelpers.GlobalScale);
            ImGui.SameLine();
            if (ImGui.InputInt($"##XIVITASanctuary_TodoList_{id}", ref amnt, 1, 2,
                    ImGuiInputTextFlags.EnterReturnsTrue)) {
                if (amnt > 0) {
                    todoList[id] = amnt;
                } else {
                    todoList.Remove(id);
                }
                
                Plugin.Configuration.TodoList = todoList;
                Plugin.Configuration.Save();
            }
            ImGui.PopItemWidth();

            ImGui.SameLine();
            ImGui.PushFont(UiBuilder.IconFont);
            var trashIcon = FontAwesomeIcon.Trash.ToIconString();
            if (ImGui.Button(trashIcon + $"##XIVITASanctuary_TodoListTrash_{id}")) {
                todoList.Remove(id);
            }
            ImGui.PopFont();

            ImGui.SameLine();
            ImGui.Text(item.Name);
        }
    }

    private void DrawAboutTab() {
        ImGui.Text("XIVITASanctuary, careto da DarkArtek.");

        if (ImGui.Button("Vedi su GitHub"))
            Process.Start(new ProcessStartInfo {
                FileName = "https://github.com/DarkArtek/XIVITASanctuary",
                UseShellExecute = true
            });

        ImGui.SameLine();

        if (ImGui.Button("Donazione"))
            Process.Start(new ProcessStartInfo {
                FileName = "https://paypal.me/ffxivita",
                UseShellExecute = true
            });
    }

    public override void Draw() {
        if (ImGui.BeginTabBar("##XIVITASanctuary_MainWindowTabs", ImGuiTabBarFlags.None)) {
            if (ImGui.BeginTabItem("Gathering")) {
                DrawGatheringTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Workshop")) {
                DrawWorkshopTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Todo")) {
                DrawTodoTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("About")) {
                DrawAboutTab();
                ImGui.EndTabItem();
            }
        }
    }
}
}