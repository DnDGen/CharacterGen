using NPCGen.Core.Data.Alignments;
using System;

namespace NPCGen.Core.Generation.Randomizers.CharacterClass
{
    public class SetClass : ICharacterClassRandomizer
    {
        public String ClassName { get; set; }

        public SetClass(String className)
        {
            ClassName = className;
        }

        public String Randomize(Alignment alignment)
        {
            return ClassName;
        }
    }
}