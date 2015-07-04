﻿using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Races;

namespace NPCGen.Generators.Interfaces.Abilities.Feats
{
    public interface IRacialFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(Race race, Dictionary<String, Skill> skills);
    }
}