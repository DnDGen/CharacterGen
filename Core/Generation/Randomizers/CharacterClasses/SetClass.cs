using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses
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