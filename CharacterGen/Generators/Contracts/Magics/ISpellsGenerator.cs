using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Magics
{
    public interface ISpellsGenerator
    {
        //Dictionary<Int32, IEnumerable<String>> GenerateFrom(CharacterClass characterClass, IEnumerable<Feat> feats, Equipment equipment);
        Dictionary<Int32, Int32> GenerateFrom(CharacterClass characterClass, Dictionary<String, Stat> stats);
    }
}
