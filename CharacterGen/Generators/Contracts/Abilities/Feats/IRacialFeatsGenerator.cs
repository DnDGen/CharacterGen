using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Abilities.Feats
{
    public interface IRacialFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(Race race, Dictionary<String, Skill> skills, Dictionary<String, Stat> stats);
    }
}