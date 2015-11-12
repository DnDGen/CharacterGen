using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Magics;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Magics
{
    public interface ISpellsGenerator
    {
        IEnumerable<Spells> GenerateFrom(CharacterClass characterClass, Dictionary<String, Stat> stats);
    }
}
