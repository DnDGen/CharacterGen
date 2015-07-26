using System;
using System.Collections.Generic;
using CharacterGen.Common.Abilities.Stats;

namespace CharacterGen.Generators.Randomizers.Stats
{
    public interface IStatsRandomizer
    {
        Dictionary<String, Stat> Randomize();
    }
}