using System;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface IRaceFactory
    {
        IBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        IMetaraceRandomizer MetaraceRandomizer { get; set; }

        Race Generate(String goodnessString, String className);
    }
}