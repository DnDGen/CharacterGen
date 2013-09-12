using System;

namespace NPCGen.Core.Characters.Generation.Randomizers.Races.Metaraces
{
    public enum METARACE_RANDOMIZER
    {
        ANY, MAYBE, ANY_LYCANTHROPE, ANY_NONLYCANTHROPE,
        ANY_GOOD, ANY_NEUTRAL, ANY_EVIL, ANY_NONGOOD, ANY_NONNEUTRAL, ANY_NONEVIL
    };

    public interface IMetaraceRandomizer
    {
        String Randomize();
    }
}