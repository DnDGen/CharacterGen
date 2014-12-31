using System;

namespace NPCGen.Tables.Interfaces
{
    public static class TableNameConstants
    {
        public static class Set
        {
            public static class Percentile
            {
                public const String AlignmentGoodness = "AlignmentGoodness";
                public const String Traits = "Traits";
            }

            public static class Collection
            {
                public const String ArmorCheckPenalties = "ArmorCheckPenalties";
                public const String BaseRaceGroups = "BaseRaceGroups";
                public const String ClassFeats = "ClassFeats";
                public const String ClassNameGroups = "ClassNameGroups";
                public const String FeatGroups = "FeatGroups";
                public const String FeatSpecificApplications = "FeatSpecificApplications";
                public const String LevelAdjustments = "LevelAdjustments";
                public const String MonsterHitDice = "MonsterHitDice";
                public const String SkillSynergyFeats = "SkillSynergyFeats";

                public static class Groups
                {
                    public const String CumulativeStrengths = "Cumulative Strengths";
                    public const String Monsters = "Monsters";
                    public const String OverwrittenStrengths = "Overwritten Strengths";
                    public const String Spellcasters = "Spellcasters";
                }
            }
        }

        public static class Formattable
        {
            public static class Percentile
            {
                public const String GOODNESSCharacterClasses = "{0}CharacterClasses";
                public const String GOODNESSCLASSBaseRaces = "{0}{1}BaseRaces";
                public const String GOODNESSCLASSMetaraces = "{0}{1}Metaraces";
            }

            public static class Collection
            {
                public const String BASERACESkillAdjustments = "{0}SkillAdjustments";
                public const String FEATSkillAdjustments = "{0}SkillAdjustments";
                public const String CLASSFeatLevelRequirements = "{0}FeatLevelRequirements";
            }
        }
    }
}