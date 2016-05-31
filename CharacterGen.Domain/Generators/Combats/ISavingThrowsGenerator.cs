using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Combats
{
    internal interface ISavingThrowsGenerator
    {
        SavingThrows GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Dictionary<string, Stat> stats);
    }
}