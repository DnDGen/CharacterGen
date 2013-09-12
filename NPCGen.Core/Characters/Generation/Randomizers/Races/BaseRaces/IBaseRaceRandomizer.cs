using System;
using NPCGen.Core.Characters.Data;

namespace NPCGen.Core.Characters.Generation.Randomizers.Races.BaseRaces
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