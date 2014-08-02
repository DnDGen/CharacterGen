﻿using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;

namespace NPCGen.Generators.Interfaces.Abilities
{
    public interface ISkillsGenerator
    {
        Dictionary<String, Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats);
    }
}