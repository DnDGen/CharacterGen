﻿using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;

namespace NPCGen.Generators.Interfaces.Abilities.Feats
{
    public interface IClassFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Dictionary<String, Stat> stats);
    }
}