using System;

namespace NPCGen.Tables.Interfaces
{
    public static class DataIndexConstants
    {
        public static class CharacterClassFeatData
        {
            public const Int32 FeatNameIndex = 0;
            public const Int32 MinimumLevelRequirementIndex = 1;
            public const Int32 FocusTypeIndex = 2;
            public const Int32 StrengthIndex = 3;
            public const Int32 FrequencyQuantityIndex = 4;
            public const Int32 FrequencyTimePeriodIndex = 5;
            public const Int32 MaximumLevelRequirementIndex = 6;
            public const Int32 FrequencyQuantityStatIndex = 7;
        }

        public static class RacialFeatData
        {
            public const Int32 FeatNameIndex = 0;
            public const Int32 SizeRequirementIndex = 1;
            public const Int32 MinimumHitDiceRequirementIndex = 2;
            public const Int32 StrengthIndex = 3;
            public const Int32 FocusIndex = 4;
            public const Int32 FrequencyQuantityIndex = 5;
            public const Int32 FrequencyTimePeriodIndex = 6;
        }

        public static class AdditionalFeatData
        {
            public const Int32 BaseAttackRequirementIndex = 0;
            public const Int32 FocusTypeIndex = 1;
            public const Int32 StrengthIndex = 2;
            public const Int32 FrequencyQuantityIndex = 3;
            public const Int32 FrequencyTimePeriodIndex = 4;
        }
    }
}