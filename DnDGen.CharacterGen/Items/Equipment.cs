using DnDGen.TreasureGen;
using DnDGen.TreasureGen.Items;

namespace DnDGen.CharacterGen.Items
{
    public class Equipment
    {
        public Weapon PrimaryHand { get; set; }
        public Item OffHand { get; set; }
        public Armor Armor { get; set; }
        public Treasure Treasure { get; set; }

        public Equipment()
        {
            Treasure = new Treasure();
        }
    }
}