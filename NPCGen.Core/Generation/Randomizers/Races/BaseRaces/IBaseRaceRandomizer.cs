using System;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public enum RACE_RANDOMIZER
    {
        ANY_STANDARD, ANY_NONSTANDARD,
        ANY_EVIL, ANY_GOOD, ANY_NEUTRAL, ANY_NONEVIL, ANY_NONNEUTRAL, ANY_NONGOOD
    };

    public interface IBaseRaceRandomizer
    {
        String Randomize(Alignment alignment, String className);
    }
}