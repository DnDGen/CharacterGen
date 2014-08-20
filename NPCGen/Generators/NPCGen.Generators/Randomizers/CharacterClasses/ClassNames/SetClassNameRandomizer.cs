using System;
using System.Collections.Generic;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class SetClassNameRandomizer : ISetClassNameRandomizer
    {
        public String SetClassName { get; set; }

        public String Randomize(Alignment alignment)
        {
            return SetClassName;
        }

        public IEnumerable<String> GetAllPossibleResults(Alignment alignment)
        {
            return new[] { SetClassName };
        }
    }
}