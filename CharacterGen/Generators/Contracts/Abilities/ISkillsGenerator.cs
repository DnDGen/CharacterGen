using System;
using System.Collections.Generic;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;

namespace CharacterGen.Generators.Abilities
{
    public interface ISkillsGenerator
    {
        Dictionary<String, Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats);
    }
}