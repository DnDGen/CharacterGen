using System;

namespace CharacterGen.Generators.Randomizers.Races
{
    public interface IForcableMetaraceRandomizer : IMetaraceRandomizer
    {
        Boolean ForceMetarace { get; set; }
    }
}