﻿using DnDGen.CharacterGen.Alignments;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Randomizers.CharacterClasses
{
    public interface IClassNameRandomizer
    {
        string Randomize(Alignment alignment);
        IEnumerable<string> GetAllPossibleResults(Alignment alignment);
    }
}