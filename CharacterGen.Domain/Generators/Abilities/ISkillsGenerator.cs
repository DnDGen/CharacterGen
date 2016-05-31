using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities
{
    internal interface ISkillsGenerator
    {
        Dictionary<string, Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats);
    }
}