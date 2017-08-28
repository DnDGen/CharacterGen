using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;

namespace CharacterGen.Characters
{
    public class CharacterPrototype
    {
        public Alignment Alignment { get; set; }
        public CharacterClassPrototype CharacterClass { get; set; }
        public RacePrototype Race { get; set; }
    }
}
