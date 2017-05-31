using CharacterGen.Skills;
using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Skills
{
    internal interface ISkillsGenerator
    {
        IEnumerable<Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities);
    }
}