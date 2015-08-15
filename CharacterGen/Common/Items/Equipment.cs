using TreasureGen.Common;
using TreasureGen.Common.Items;

namespace CharacterGen.Common.Items
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
            Treasure = new Treasure();
        }
    }
}