using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;

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