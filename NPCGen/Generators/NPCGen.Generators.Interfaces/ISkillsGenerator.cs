using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Skills;
using NPCGen.Common.Abilities;

namespace NPCGen.Generators.Interfaces
{
    public interface ISkillsGenerator
    {
        Dictionary<String, Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats);
    }
}