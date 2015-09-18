using System;

namespace CharacterGen.Generators.Randomizers.Races
{
    public interface ISetMetaraceRandomizer : RaceRandomizer
    {
        String SetMetarace { get; set; }
    }
}