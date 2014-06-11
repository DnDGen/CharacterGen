﻿using System;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;

namespace NPCGen.Generators.Interfaces
{
    public interface IHitPointsFactory
    {
        Int32 CreateWith(CharacterClass characterClass, Int32 constitutionBonus, Race race);
    }
}