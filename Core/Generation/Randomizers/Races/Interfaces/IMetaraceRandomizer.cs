﻿using System;

namespace NPCGen.Core.Generation.Randomizers.Races.Interfaces
{
    public enum METARACE_RANDOMIZER
    {
        ANY_LYCANTHROPE, ANY_NONLYCANTHROPE,
        ANY_GOOD, ANY_NEUTRAL, ANY_NONGOOD, ANY_NONNEUTRAL, ANY_NONEVIL
    };

    public interface IMetaraceRandomizer
    {
        String Randomize(String goodnessString, String className);
    }
}