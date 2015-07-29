using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Abilities.Feats
{
    public interface IClassFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, IEnumerable<Feat> racialFeats, Dictionary<String, Skill> skills);
    }
}