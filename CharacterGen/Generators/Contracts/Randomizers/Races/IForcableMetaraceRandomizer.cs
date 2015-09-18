using System;

namespace CharacterGen.Generators.Randomizers.Races
{
    public interface IForcableMetaraceRandomizer : RaceRandomizer
    {
        Boolean ForceMetarace { get; set; }
    }
}