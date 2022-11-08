using System;
using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using XIVITAGuide.Attributes;
using XIVITAGuide.Localization;
using XIVITAGuide.Types;
using XIVITAGuide.UI.ImGuiBasicComponents;

namespace XIVITAGuide.UI.ImGuiFullComponents.MechanicTable
{
    public static class MechanicTableComponent
    {
        public static void Draw(List<Guide.Section.Phase.Mechanic> mechanics)
        {
            try
            {
                var hiddenMechanics = MechanicTablePresenter.Configuration.Display.HiddenMechanics;
                var shortMode = MechanicTablePresenter.Configuration.Accessiblity.ShortenGuideText;

                if (!mechanics.All(x => hiddenMechanics?.Contains((GuideMechanics)x.Type) ?? false))
                {

                    if (ImGui.BeginTable("##MechanicTableComponentMechTable", 3, ImGuiTableFlags.Hideable | ImGuiTableFlags.Reorderable | ImGuiTableFlags.Borders | ImGuiTableFlags.Resizable))
                    {
                        ImGui.TableSetupColumn(label: TGenerics.Mechanic);
                        ImGui.TableSetupColumn(label: TGenerics.Description);
                        ImGui.TableSetupColumn(TGenerics.Type);
                        ImGui.TableHeadersRow();
                        foreach (var mechanic in mechanics)
                        {
                            if (hiddenMechanics?.Contains((GuideMechanics)mechanic.Type) == true)
                            {
                                continue;
                            }

                            ImGui.TableNextRow();
                            ImGui.TableNextColumn();
                            ImGui.Text(mechanic.Name);
                            ImGui.TableNextColumn();
                            ImGui.TextWrapped((shortMode && mechanic.ShortDescription != null) ? mechanic.ShortDescription : mechanic.Description);
                            ImGui.TableNextColumn();
                            ImGui.Text(AttributeExtensions.GetNameAttribute((GuideMechanics)mechanic.Type));
                            Common.AddTooltip(AttributeExtensions.GetDescriptionAttribute((GuideMechanics)mechanic.Type));
                        }

                        ImGui.EndTable();
                    }
                }
            }
            catch (Exception e) { ImGui.TextColored(Colours.Error, $"Component Exception: {e.Message}"); }
        }
    }
}