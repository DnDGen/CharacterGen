using System;
using System.Collections.Generic;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Generators.Combats;

namespace CharacterGen.Generators.Domain.Combats
{
    public class SavingThrowsGenerator : ISavingThrowsGenerator
    {
        public SavingThrows GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Dictionary<String, Stat> stats)
        {
            throw new NotImplementedException();
        }
    }
}