using System;

namespace NPCGen.Generators.Interfaces.Randomizers.Races
{
    public interface ISetBaseRaceRandomizer : IBaseRaceRandomizer
    {
        String SetBaseRaceId { get; set; }
    }
}