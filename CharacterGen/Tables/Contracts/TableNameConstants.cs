using System;

namespace CharacterGen.Tables
{
    public static class TableNameConstants
    {
        public static class Set
        {
            public static class Adjustments
            {
                public const String ArmorBonuses = "ArmorBonuses";
                public const String ArmorCheckPenalties = "ArmorCheckPenalties";
                public const String ClassHitDice = "ClassHitDice";
                public const String FeatArmorAdjustments = "FeatArmorAdjustments";
                public const String FeatInitiativeBonuses = "FeatInitiativeBonuses";
                public const String FighterFeatLevelRequirements = "FighterFeatLevelRequirements";
                public const String LandSpeeds = "LandSpeeds";
                public const String LeadershipModifiers = "LeadershipModifiers";
                public const String LevelAdjustments = "LevelAdjustments";
                public const String MaxDexterityBonus = "MaxDexterityBonus";
                public const String MonsterHitDice = "MonsterHitDice";
                public const String ProhibitedFieldQuantities = "ProhibitedFieldQuantities";
                public const String RacialBaseAttackAdjustments = "RacialBaseAttackAdjustments";
                public const String RacialInitiativeBonuses = "RacialInitiativeBonuses";
                public const String RacialNaturalArmorBonuses = "RacialNaturalArmorBonuses";
                public const String SkillPointsForClasses = "SkillPointsForClasses";
                public const String SpecialistFieldQuantities = "SpecialistFieldQuantities";
            }

            public static class Collection
            {
                public const String AdditionalFeatData = "AdditionalFeatData";
                public const String AlignmentGroups = "AlignmentGroups";
                public const String ArmorClassModifiers = "ArmorClassModifiers";
                public const String AutomaticLanguages = "AutomaticLanguages";
                public const String BaseRaceGroups = "BaseRaceGroups";
                public const String BonusLanguages = "BonusLanguages";
                public const String ClassNameGroups = "ClassNameGroups";
                public const String ClassSkills = "ClassSkills";
                public const String CrossClassSkills = "CrossClassSkills";
                public const String DragonSpecies = "DragonSpecies";
                public const String EquivalentFeats = "EquivalentFeats";
                public const String FeatFoci = "FeatFoci";
                public const String FeatGroups = "FeatGroups";
                public const String MetaraceGroups = "MetaraceGroups";
                public const String Names = "Names";
                public const String ProhibitedFields = "ProhibitedFields";
                public const String RacialFeatHitDieRequirements = "RacialFeatHitDieRequirements";
                public const String RequiredFeats = "RequiredFeats";
                public const String SkillData = "SkillData";
                public const String SkillGroups = "SkillGroups";
                public const String SkillSynergy = "SkillSynergy";
                public const String SpecialistFields = "SpecialistFields";
                public const String StatPriorities = "StatPriorities";
            }

            public static class Percentile
            {
                public const String AlignmentGoodness = "AlignmentGoodness";
                public const String LeadershipMovement = "LeadershipMovement";
                public const String Reputation = "Reputation";
                public const String Traits = "Traits";
            }

            public static class TrueOrFalse
            {
                public const String AssignPointToCrossClassSkill = "AssignPointToCrossClassSkill";
                public const String AttractCohortOfDifferentAlignment = "AttractCohortOfDifferentAlignment";
                public const String IncreaseFirstPriorityStat = "IncreaseFirstPriorityStat";
                public const String KilledCohort = "KilledCohort";
                public const String KilledFollowers = "KilledFollowers";
                public const String Male = "Male";
            }
        }

        public static class Formattable
        {
            public static class Adjustments
            {
                public const String CLASSFeatLevelRequirements = "{0}FeatLevelRequirements";
                public const String FEATClassRequirements = "{0}ClassRequirements";
                public const String FEATSkillRankRequirements = "{0}SkillRankRequirements";
                public const String FEATStatRequirements = "{0}StatRequirements";
                public const String STATStatAdjustments = "{0}StatAdjustments";
            }

            public static class Collection
            {
                public const String RACEFeatData = "{0}FeatData";
                public const String CLASSFeatData = "{0}FeatData";
            }

            public static class Percentile
            {
                public const String GOODNESSCharacterClasses = "{0}CharacterClasses";
                public const String GOODNESSCLASSBaseRaces = "{0}{1}BaseRaces";
                public const String GOODNESSCLASSMetaraces = "{0}{1}Metaraces";
            }

            public static class TrueOrFalse
            {
                public const String CLASSHasSpecialistFields = "{0}HasSpecialistFields";
            }
        }
    }
}