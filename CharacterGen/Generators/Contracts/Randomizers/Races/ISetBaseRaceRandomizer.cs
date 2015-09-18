using System;

namespace CharacterGen.Generators.Randomizers.Races
{
    public interface ISetBaseRaceRandomizer : RaceRandomizer
    {
        String SetBaseRace { get; set; }
    }
}