using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Races;

namespace DnDGen.CharacterGen.Characters
{
    public class CharacterPrototype
    {
        public Alignment Alignment { get; set; }
        public CharacterClassPrototype CharacterClass { get; set; }
        public RacePrototype Race { get; set; }
    }
}
