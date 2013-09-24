using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public interface IBaseRaceRandomizer
    {
        String Randomize(Alignment alignment, String className);
    }
}