using System;
using System.Collections.Generic;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface IStatsFactory
    {
        Dictionary<String, Stat> CreateWith(IStatsRandomizer statsRandomizer, CharacterClass characterClass, Race race);
    }
}