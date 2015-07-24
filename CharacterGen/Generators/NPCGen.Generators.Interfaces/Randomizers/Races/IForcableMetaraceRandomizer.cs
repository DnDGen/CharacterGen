using System;

namespace NPCGen.Generators.Interfaces.Randomizers.Races
{
    public interface IForcableMetaraceRandomizer : IMetaraceRandomizer
    {
        Boolean ForceMetarace { get; set; }
    }
}