using System;
using System.Collections.Generic;
using NPCGen.Common.Alignments;

namespace NPCGen.Generators.Interfaces.Randomizers.CharacterClasses
{
    public interface IClassNameRandomizer
    {
        String Randomize(Alignment alignment);
        IEnumerable<String> GetAllPossibleResults(Alignment alignment);
    }
}