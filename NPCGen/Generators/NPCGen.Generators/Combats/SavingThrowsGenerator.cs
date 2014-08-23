using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Generators.Interfaces.Combats;

namespace NPCGen.Generators.Combats
{
    public class SavingThrowsGenerator : ISavingThrowsGenerator
    {
        public SavingThrows GenerateWith(CharacterClass characterClass, IEnumerable<String> feats, Dictionary<String, Stat> stats)
        {
            throw new NotImplementedException();
        }
    }
}