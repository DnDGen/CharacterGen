using System;
using System.Collections.Generic;
using System.Linq;
using EquipmentGen.Common;
using EquipmentGen.Common.Items;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;

namespace NPCGen.Common
{
    public class Character
    {
        public Alignment Alignment { get; set; }
        public ArmorClass ArmorClass { get; set; }
        public CharacterClass Class { get; set; }
        public Familiar Familiar { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public Int32 HitPoints { get; set; }
        public String InterestingTrait { get; set; }
        public IEnumerable<String> Languages { get; set; }
        public Race Race { get; set; }
        public Dictionary<String, Skill> Skills { get; set; }
        public Dictionary<String, Stat> Stats { get; set; }
        public Item PrimaryHand { get; set; }
        public Item OffHand { get; set; }
        public Item Armor { get; set; }
        public Treasure Treasure { get; set; }
        public Dictionary<Int32, IEnumerable<String>> Spells { get; set; }
        public SavingThrows SavingThrows { get; set; }

        public Character()
        {
            Alignment = new Alignment();
            Class = new CharacterClass();
            Familiar = new Familiar();
            Feats = Enumerable.Empty<Feat>();
            InterestingTrait = String.Empty;
            Languages = Enumerable.Empty<String>();
            Race = new Race();
            Skills = new Dictionary<String, Skill>();
            Stats = new Dictionary<String, Stat>();
            PrimaryHand = new Item();
            OffHand = new Item();
            Armor = new Item();
            Treasure = new Treasure();
            Spells = new Dictionary<Int32, IEnumerable<String>>();
        }
    }
}