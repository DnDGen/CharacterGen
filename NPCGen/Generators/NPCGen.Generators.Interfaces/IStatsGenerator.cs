using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces.Randomizers.Stats;

namespace NPCGen.Generators.Interfaces
{
    public interface IStatsGenerator
    {
        Dictionary<String, Stat> GenerateWith(IStatsRandomizer statsRandomizer, CharacterClass characterClass, Race race);
    }
}