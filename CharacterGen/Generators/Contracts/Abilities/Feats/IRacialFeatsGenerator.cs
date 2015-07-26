using System;
using System.Collections.Generic;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Races;

namespace CharacterGen.Generators.Abilities.Feats
{
    public interface IRacialFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(Race race, Dictionary<String, Skill> skills);
    }
}