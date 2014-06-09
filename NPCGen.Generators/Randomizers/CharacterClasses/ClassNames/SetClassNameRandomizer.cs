using System;
using System.Collections.Generic;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class SetClassNameRandomizer : IClassNameRandomizer
    {
        public String ClassName { get; set; }

        public String Randomize(Alignment alignment)
        {
            return ClassName;
        }

        public IEnumerable<String> GetAllPossibleResults(Alignment alignment)
        {
            return new[] { ClassName };
        }
    }
}