using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;

namespace NPCGen.Generators.Interfaces.Combats
{
    public interface ISavingThrowsGenerator
    {
        SavingThrows GenerateWith(CharacterClass characterClass, IEnumerable<String> feats, Dictionary<String, Stat> stats);
    }
}