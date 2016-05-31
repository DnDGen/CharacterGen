using CharacterGen.Abilities;
using CharacterGen.Abilities.Feats;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Items;
using CharacterGen.Magics;
using CharacterGen.Races;
using System.Linq;

namespace CharacterGen
{
    public class Character
    {
        public Alignment Alignment { get; set; }
        public CharacterClass Class { get; set; }
        public Race Race { get; set; }
        public string InterestingTrait { get; set; }

        public Combat Combat { get; set; }
        public Ability Ability { get; set; }
        public Equipment Equipment { get; set; }
        public Magic Magic { get; set; }

        public bool IsLeader
        {
            get
            {
                return Ability.Feats.Any(f => f.Name == FeatConstants.Leadership);
            }
        }

        public Character()
        {
            Alignment = new Alignment();
            Class = new CharacterClass();
            Race = new Race();
            InterestingTrait = string.Empty;
            Combat = new Combat();
            Ability = new Ability();
            Equipment = new Equipment();
            Magic = new Magic();
        }
    }
}