using CharacterGen.Abilities.Skills;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities
{
    internal interface ILanguageGenerator
    {
        IEnumerable<string> GenerateWith(Race race, string className, int intelligenceBonus, IEnumerable<Skill> skills);
    }
}