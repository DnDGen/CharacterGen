using System;
using System.Collections.Generic;
using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames
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