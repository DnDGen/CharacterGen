using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Skills;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Languages
{
    internal interface ILanguageGenerator
    {
        IEnumerable<string> GenerateWith(Race race, CharacterClass characterClass, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills);
    }
}