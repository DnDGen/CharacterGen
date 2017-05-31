using CharacterGen.CharacterClasses;
using CharacterGen.Feats;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Combats
{
    internal interface IHitPointsGenerator
    {
        int GenerateWith(CharacterClass characterClass, int constitutionBonus, Race race, IEnumerable<Feat> feats);
    }
}