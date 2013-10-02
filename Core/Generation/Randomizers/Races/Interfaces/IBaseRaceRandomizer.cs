using System;

namespace NPCGen.Core.Generation.Randomizers.Races.Interfaces
{
    public interface IBaseRaceRandomizer
    {
        String Randomize(String goodnessString, String className);
    }
}