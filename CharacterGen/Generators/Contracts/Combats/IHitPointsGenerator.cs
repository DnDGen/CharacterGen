using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Combats
{
    public interface IHitPointsGenerator
    {
        Int32 GenerateWith(CharacterClass characterClass, Int32 constitutionBonus, Race race,
            IEnumerable<Feat> feats);
    }
}