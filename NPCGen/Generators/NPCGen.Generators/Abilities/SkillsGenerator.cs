using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities;

namespace NPCGen.Generators.Abilities
{
    public class SkillsGenerator : ISkillsGenerator
    {
        public Dictionary<String, Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats)
        {
            throw new NotImplementedException();
        }
    }
}