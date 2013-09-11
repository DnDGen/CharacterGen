using System;

namespace NPCGen.Core.Characters.Generation.Randomizers.Races
{
    public enum RACE_RANDOMIZER
    {
        ANY, ANY_STANDARD, ANY_NONSTANDARD,
        ANY_EVIL, ANY_GOOD, ANY_NEUTRAL, ANY_NONEVIL, ANY_NONNEUTRAL, ANY_NONGOOD
    };

    public interface IRaceRandomizer
    {
        String Randomize();
    }
}