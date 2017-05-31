using CharacterGen.Feats;
using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Combats
{
    internal interface ISavingThrowsGenerator
    {
        SavingThrows GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities);
    }
}