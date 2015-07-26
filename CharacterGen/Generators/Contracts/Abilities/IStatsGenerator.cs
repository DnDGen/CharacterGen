using System;
using System.Collections.Generic;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Stats;

namespace CharacterGen.Generators.Abilities
{
    public interface IStatsGenerator
    {
        Dictionary<String, Stat> GenerateWith(IStatsRandomizer statsRandomizer, CharacterClass characterClass, Race race);
    }
}