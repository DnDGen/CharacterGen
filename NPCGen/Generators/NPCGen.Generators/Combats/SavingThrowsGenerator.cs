using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Generators.Interfaces.Combats;

namespace NPCGen.Generators.Combats
{
    public class SavingThrowsGenerator : ISavingThrowsGenerator
    {
        public SavingThrows GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Dictionary<String, Stat> stats)
        {
            throw new NotImplementedException();
        }
    }
}