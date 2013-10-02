using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Stats;

namespace NPCGen.Core.Generation.Randomizers.Stats.Interfaces
{
    public enum ROLLMETHOD { BEST_OF_4, ONE_AS_6, STRAIGHT, TWO_d10, AVERAGE, MEDIUM, HEROIC, POOR };

    public interface IStatsRandomizer
    {
        Dictionary<String, Stat> Randomize();
    }
}