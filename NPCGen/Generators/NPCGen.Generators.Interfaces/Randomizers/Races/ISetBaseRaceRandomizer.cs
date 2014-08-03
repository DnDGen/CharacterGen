using System;

namespace NPCGen.Generators.Interfaces.Randomizers.Races
{
    public interface ISetBaseRaceRandomizer : IBaseRaceRandomizer
    {
        String SetBaseRace { get; set; }
    }
}