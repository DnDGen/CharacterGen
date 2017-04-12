using TreasureGen;
using TreasureGen.Items;

namespace CharacterGen.Items
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