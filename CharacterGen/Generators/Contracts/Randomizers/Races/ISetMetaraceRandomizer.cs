using System;

namespace CharacterGen.Generators.Randomizers.Races
{
    public interface ISetMetaraceRandomizer : IMetaraceRandomizer
    {
        String SetMetarace { get; set; }
    }
}