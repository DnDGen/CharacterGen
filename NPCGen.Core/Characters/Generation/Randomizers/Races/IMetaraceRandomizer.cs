using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Generation.Randomizers.Races
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