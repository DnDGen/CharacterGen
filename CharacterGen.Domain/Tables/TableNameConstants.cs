﻿namespace CharacterGen.Domain.Tables
{
    internal static class TableNameConstants
    {
        internal static class Set
        {
            internal static class Adjustments
            {
                public const string ArcaneSpellFailures = "ArcaneSpellFailures";
                public const string ArmorBonuses = "ArmorBonuses";
                public const string ArmorCheckPenalties = "ArmorCheckPenalties";
                public const string ClassHitDice = "ClassHitDice";
                public const string CohortLevels = "CohortLevels";
                public const string FeatArmorAdjustments = "FeatArmorAdjustments";
                public const string FeatInitiativeBonuses = "FeatInitiativeBonuses";
                public const string FighterFeatLevelRequirements = "FighterFeatLevelRequirements";
                public const string LandSpeeds = "LandSpeeds";
                public const string LeadershipModifiers = "LeadershipModifiers";
                public const string LevelAdjustments = "LevelAdjustments";
                public const string MaxDexterityBonus = "MaxDexterityBonus";
                public const string MonsterHitDice = "MonsterHitDice";
                public const string ProhibitedFieldQuantities = "ProhibitedFieldQuantities";
                public const string RacialBaseAttackAdjustments = "RacialBaseAttackAdjustments";
                public const string SkillPointsForClasses = "SkillPointsForClasses";
                public const string SpecialistFieldQuantities = "SpecialistFieldQuantities";
            }

            internal static class Collection
            {
                public const string AdditionalFeatData = "AdditionalFeatData";
                public const string AlignmentGroups = "AlignmentGroups";
                public const string Animals = "Animals";
                public const string ArmorClassModifiers = "ArmorClassModifiers";
                public const string AutomaticLanguages = "AutomaticLanguages";
                public const string BaseRaceGroups = "BaseRaceGroups";
                public const string BonusLanguages = "BonusLanguages";
                public const string ClassNameGroups = "ClassNameGroups";
                public const string ClassSkills = "ClassSkills";
                public const string CrossClassSkills = "CrossClassSkills";
                public const string DragonSpecies = "DragonSpecies";
                public const string EquivalentFeats = "EquivalentFeats";
                public const string FeatFoci = "FeatFoci";
                public const string FeatGroups = "FeatGroups";
                public const string HeightRolls = "HeightRolls";
                public const string ItemGroups = "ItemGroups";
                public const string MaximumAgeRolls = "MaximumAgeRolls";
                public const string MetaraceGroups = "MetaraceGroups";
                public const string Names = "Names";
                public const string ProhibitedFields = "ProhibitedFields";
                public const string RacialFeatHitDieRequirements = "RacialFeatHitDieRequirements";
                public const string RequiredFeats = "RequiredFeats";
                public const string SkillData = "SkillData";
                public const string SkillGroups = "SkillGroups";
                public const string SkillSynergy = "SkillSynergy";
                public const string SpecialistFields = "SpecialistFields";
                public const string SpellGroups = "SpellGroups";
                public const string StatGroups = "StatGroups";
                public const string StatPriorities = "StatPriorities";
                public const string WeightRolls = "WeightRolls";
            }

            internal static class Percentile
            {
                public const string AlignmentGoodness = "AlignmentGoodness";
                public const string AlignmentLawfulness = "AlignmentLawfulness";
                public const string LeadershipMovement = "LeadershipMovement";
                public const string Reputation = "Reputation";
                public const string Traits = "Traits";
            }

            internal static class TrueOrFalse
            {
                public const string AssignPointToCrossClassSkill = "AssignPointToCrossClassSkill";
                public const string AttractCohortOfDifferentAlignment = "AttractCohortOfDifferentAlignment";
                public const string IncreaseFirstPriorityStat = "IncreaseFirstPriorityStat";
                public const string KilledCohort = "KilledCohort";
                public const string KilledFollowers = "KilledFollowers";
                public const string Male = "Male";
            }
        }

        internal static class Formattable
        {
            internal static class Adjustments
            {
                public const string AGEStatAdjustments = "{0}StatAdjustments";
                public const string CLASSFeatLevelRequirements = "{0}FeatLevelRequirements";
                public const string CLASSSpellLevels = "{0}SpellLevels";
                public const string FEATClassRequirements = "{0}ClassRequirements";
                public const string FEATSkillRankRequirements = "{0}SkillRankRequirements";
                public const string FEATStatRequirements = "{0}StatRequirements";
                public const string GENDERHeights = "{0}Heights";
                public const string GENDERWeights = "{0}Weights";
                public const string LevelXAnimalTricks = "Level{0}AnimalTricks";
                public const string LevelXCLASSSpellsPerDay = "Level{0}{1}SpellsPerDay";
                public const string LevelXCLASSKnownSpells = "Level{0}{1}KnownSpells";
                public const string LevelXFollowerQuantities = "Level{0}FollowerQuantities";
                public const string STATStatAdjustments = "{0}StatAdjustments";
                public const string RACEAges = "{0}Ages";
            }

            internal static class Collection
            {
                public const string RACEFeatData = "{0}FeatData";
                public const string CLASSFeatData = "{0}FeatData";
                public const string CLASSTYPEAgeRolls = "{0}AgeRolls";
            }

            internal static class Percentile
            {
                public const string GOODNESSCharacterClasses = "{0}CharacterClasses";
                public const string GOODNESSCLASSBaseRaces = "{0}{1}BaseRaces";
                public const string GOODNESSCLASSMetaraces = "{0}{1}Metaraces";
                public const string LevelXPower = "Level{0}Power";
            }

            internal static class TrueOrFalse
            {
                public const string CLASSHasSpecialistFields = "{0}HasSpecialistFields";
                public const string CLASSKnowsAdditionalSpells = "{0}KnowsAdditionalSpells";
            }
        }
    }
}