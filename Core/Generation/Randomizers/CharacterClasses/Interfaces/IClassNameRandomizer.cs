using NPCGen.Core.Data.Alignments;
using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces
{
    public interface IClassNameRandomizer
    {
        String Randomize(Alignment alignment);
        IEnumerable<String> GetAllPossibleResults(Alignment alignment);
    }
}