using System;
using System.Collections.Generic;
using CharacterGen.Common.Races;

namespace CharacterGen.Generators.Abilities
{
    public interface ILanguageGenerator
    {
        IEnumerable<String> GenerateWith(Race race, String className, Int32 intelligenceBonus);
    }
}