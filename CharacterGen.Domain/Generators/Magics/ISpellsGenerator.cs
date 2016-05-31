using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Magics;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Magics
{
    internal interface ISpellsGenerator
    {
        IEnumerable<Spells> GenerateFrom(CharacterClass characterClass, Dictionary<string, Stat> stats);
    }
}
