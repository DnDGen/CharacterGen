﻿using System;

namespace CharacterGen.Generators.Randomizers.Stats
{
    public interface ISetStatsRandomizer : IStatsRandomizer
    {
        Int32 SetStrength { get; set; }
        Int32 SetDexterity { get; set; }
        Int32 SetConstitution { get; set; }
        Int32 SetIntelligence { get; set; }
        Int32 SetWisdom { get; set; }
        Int32 SetCharisma { get; set; }
        Boolean AllowAdjustments { get; set; }
    }
}