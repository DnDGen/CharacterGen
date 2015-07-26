using System;
using System.Collections.Generic;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;

namespace CharacterGen.Generators.Combats
{
    public interface ISavingThrowsGenerator
    {
        SavingThrows GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Dictionary<String, Stat> stats);
    }
}