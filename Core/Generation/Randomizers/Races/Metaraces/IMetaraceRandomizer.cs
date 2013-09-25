using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public enum METARACE_RANDOMIZER
    {
        ANY, MAYBE, ANY_LYCANTHROPE, ANY_NONLYCANTHROPE,
        ANY_GOOD, ANY_NEUTRAL, ANY_EVIL, ANY_NONGOOD, ANY_NONNEUTRAL, ANY_NONEVIL
    };

    public interface IMetaraceRandomizer
    {
        String Randomize(Alignment alignment, String className);
    }
}