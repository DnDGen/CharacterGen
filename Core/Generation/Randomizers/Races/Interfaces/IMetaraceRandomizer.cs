using System;

namespace NPCGen.Core.Generation.Randomizers.Races.Interfaces
{
    public interface IMetaraceRandomizer
    {
        String Randomize(String goodnessString, String className);
    }
}