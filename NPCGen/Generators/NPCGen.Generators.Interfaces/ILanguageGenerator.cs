using System;
using System.Collections.Generic;
using NPCGen.Common.Races;

namespace NPCGen.Generators.Interfaces
{
    public interface ILanguageGenerator
    {
        IEnumerable<String> GenerateWith(Race race, String className, Int32 intelligenceBonus);
    }
}