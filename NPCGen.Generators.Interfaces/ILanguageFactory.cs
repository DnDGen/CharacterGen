using System;
using System.Collections.Generic;
using NPCGen.Common.Races;

namespace NPCGen.Generators.Interfaces
{
    public interface ILanguageFactory
    {
        IEnumerable<String> CreateWith(Race race, String className, Int32 intelligenceBonus);
    }
}