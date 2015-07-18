using System;

namespace NPCGen.Generators.Interfaces.Randomizers.Races
{
    public interface ISetMetaraceRandomizer : IMetaraceRandomizer
    {
        String SetMetarace { get; set; }
    }
}