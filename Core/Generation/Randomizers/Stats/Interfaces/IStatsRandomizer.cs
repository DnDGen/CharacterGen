using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Stats;

namespace NPCGen.Core.Generation.Randomizers.Stats.Interfaces
{
    public interface IStatsRandomizer
    {
        Dictionary<String, Stat> Randomize();
    }
}