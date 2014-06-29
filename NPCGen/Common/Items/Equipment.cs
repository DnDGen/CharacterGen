using EquipmentGen.Common;
using EquipmentGen.Common.Items;

namespace NPCGen.Common.Items
{
    public class Equipment
    {
        public Item PrimaryHand { get; set; }
        public Item OffHand { get; set; }
        public Item Armor { get; set; }
        public Treasure Treasure { get; set; }

        public Equipment()
        {
            PrimaryHand = new Item();
            OffHand = new Item();
            Armor = new Item();
            Treasure = new Treasure();
        }
    }
}