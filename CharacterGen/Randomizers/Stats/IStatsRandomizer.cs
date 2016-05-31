using CharacterGen.Abilities.Stats;
using System.Collections.Generic;

namespace CharacterGen.Randomizers.Stats
{
    public interface IStatsRandomizer
    {
        Dictionary<string, Stat> Randomize();
    }
}