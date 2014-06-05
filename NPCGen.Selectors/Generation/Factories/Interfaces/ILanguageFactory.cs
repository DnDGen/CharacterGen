using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface ILanguageFactory
    {
        IEnumerable<String> CreateWith(Race race, String className, Int32 intelligenceBonus);
    }
}