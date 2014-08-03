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
            throw new NotImplementedException();
        }

        public IEnumerable<String> GetAllPossibleResults(Alignment alignment)
        {
            throw new NotImplementedException();
        }
    }
}