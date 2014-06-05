using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames
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