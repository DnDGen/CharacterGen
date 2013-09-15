using NPCGen.Core.Data.Stats;
using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.Stats
{
    public interface IStatsRandomizer
    {
        Dictionary<String, Stat> Randomize();
    }
}