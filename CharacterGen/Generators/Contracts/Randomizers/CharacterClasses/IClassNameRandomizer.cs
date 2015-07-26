using System;
using System.Collections.Generic;
using CharacterGen.Common.Alignments;

namespace CharacterGen.Generators.Randomizers.CharacterClasses
{
    public interface IClassNameRandomizer
    {
        String Randomize(Alignment alignment);
        IEnumerable<String> GetAllPossibleResults(Alignment alignment);
    }
}