using System;

namespace NPCGen.Generators.Interfaces.Randomizers.Races
{
    public interface ISetMetaraceRandomizer : IMetaraceRandomizer
    {
        String SetMetaraceId { get; set; }
    }
}