using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;

namespace NPCGen.Tests.Integration.Common
{
    public class DependentDataCollection
    {
        public Alignment Alignment { get; set; }
        public CharacterClassPrototype CharacterClassPrototype { get; set; }
        public CharacterClass CharacterClass { get; set; }
        public Race Race { get; set; }
    }
}