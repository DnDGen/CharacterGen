using System;
using CharacterGen.Common.Abilities;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Items;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;

namespace CharacterGen.Common
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