﻿namespace XIVITASanctuary
{
    using ImGuiScene;
    using Lumina.Excel.GeneratedSheets;
    
    public abstract class BaseItem {
        public string Name;
        public Item Item;
        public uint ItemID;
        public uint RowID;
        public byte UIIndex;
    }
}