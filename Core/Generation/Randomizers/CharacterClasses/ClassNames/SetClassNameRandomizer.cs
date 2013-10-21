using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }
    }
}