using System;
using NPCGen.Common.Abilities;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Magics;
using NPCGen.Common.Races;

namespace NPCGen.Common
{
    public class Character
    {
        public Alignment Alignment { get; set; }
        public CharacterClass Class { get; set; }
        public Race Race { get; set; }
        public String InterestingTrait { get; set; }

        public Combat Combat { get; set; }
        public Ability Ability { get; set; }
        public Equipment Equipment { get; set; }
        public Magic Magic { get; set; }
        public Leadership Leadership { get; set; }

        public Character()
        {
            Alignment = new Alignment();
            Class = new CharacterClass();
            Race = new Race();
            InterestingTrait = String.Empty;
            Combat = new Combat();
            Ability = new Ability();
            Equipment = new Equipment();
            Magic = new Magic();
            Leadership = new Leadership();
        }
    }
}