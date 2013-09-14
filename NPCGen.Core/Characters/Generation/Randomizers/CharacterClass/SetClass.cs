using NPCGen.Core.Characters.Data.Alignments;
using System;

namespace NPCGen.Core.Characters.Generation.Randomizers.CharacterClass
{
    public class SetClass : IClassRandomizer
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