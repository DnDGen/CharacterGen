using System;

namespace CharacterGen.Generators.Randomizers.Races
{
    public interface ISetBaseRaceRandomizer : IBaseRaceRandomizer
    {
        String SetBaseRace { get; set; }
    }
}