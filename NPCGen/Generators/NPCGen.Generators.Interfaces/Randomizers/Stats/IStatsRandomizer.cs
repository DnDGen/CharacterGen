using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Stats;

namespace NPCGen.Generators.Interfaces.Randomizers.Stats
{
    public interface IStatsRandomizer
    {
        Dictionary<String, Stat> Randomize();
    }
}