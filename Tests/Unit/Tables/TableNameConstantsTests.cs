using System;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Tables
{
    [TestFixture]
    public class TableNameConstantsTests
    {
        [TestCase(TableNameConstants.Set.Collection.ArmorBonuses, "ArmorBonuses")]
        [TestCase(TableNameConstants.Set.Collection.ArmorCheckPenalties, "ArmorCheckPenalties")]
        [TestCase(TableNameConstants.Set.Collection.ArmorClassModifiers, "ArmorClassModifiers")]
        [TestCase(TableNameConstants.Set.Collection.BaseRaceGroups, "BaseRaceGroups")]
        [TestCase(TableNameConstants.Set.Collection.ClassFeats, "ClassFeats")]
        [TestCase(TableNameConstants.Set.Collection.ClassHitDice, "ClassHitDice")]
        [TestCase(TableNameConstants.Set.Collection.ClassNameGroups, "ClassNameGroups")]
        [TestCase(TableNameConstants.Set.Collection.ClassSkills, "ClassSkills")]
        [TestCase(TableNameConstants.Set.Collection.CrossClassSkills, "CrossClassSkills")]
        [TestCase(TableNameConstants.Set.Collection.DragonSpecies, "DragonSpecies")]
        [TestCase(TableNameConstants.Set.Collection.FeatArmorAdjustments, "FeatArmorAdjustments")]
        [TestCase(TableNameConstants.Set.Collection.FeatGroups, "FeatGroups")]
        [TestCase(TableNameConstants.Set.Collection.FeatInitiativeBonuses, "FeatInitiativeBonuses")]
        [TestCase(TableNameConstants.Set.Collection.FeatSpecificApplications, "FeatSpecificApplications")]
        [TestCase(TableNameConstants.Set.Collection.LandSpeeds, "LandSpeeds")]
        [TestCase(TableNameConstants.Set.Collection.LevelAdjustments, "LevelAdjustments")]
        [TestCase(TableNameConstants.Set.Collection.MaxDexterityBonus, "MaxDexterityBonus")]
        [TestCase(TableNameConstants.Set.Collection.MetaraceGroups, "MetaraceGroups")]
        [TestCase(TableNameConstants.Set.Collection.MonsterHitDice, "MonsterHitDice")]
        [TestCase(TableNameConstants.Set.Collection.RacialBaseAttackAdjustments, "RacialBaseAttackAdjustments")]
        [TestCase(TableNameConstants.Set.Collection.RacialInitiativeBonuses, "RacialInitiativeBonuses")]
        [TestCase(TableNameConstants.Set.Collection.RacialNaturalArmorBonuses, "RacialNaturalArmorBonuses")]
        [TestCase(TableNameConstants.Set.Collection.SkillGroups, "SkillGroups")]
        [TestCase(TableNameConstants.Set.Collection.SkillPointsForClasses, "SkillPointsForClasses")]
        [TestCase(TableNameConstants.Set.Collection.SkillSynergy, "SkillSynergy")]
        [TestCase(TableNameConstants.Set.Collection.SkillSynergyFeats, "SkillSynergyFeats")]
        [TestCase(TableNameConstants.Set.Collection.StatPriorities, "StatPriorities")]
        [TestCase(TableNameConstants.Set.Collection.Groups.AverageBaseAttack, "AverageBaseAttack")]
        [TestCase(TableNameConstants.Set.Collection.Groups.CumulativeStrengths, "CumulativeStrengths")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Deflection, "Deflection")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Dodge, "Dodge")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Genetic, "Genetic")]
        [TestCase(TableNameConstants.Set.Collection.Groups.GoodBaseAttack, "GoodBaseAttack")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Healers, "Healers")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Knowledge, "Knowledge")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Lycanthrope, "Lycanthrope")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Mages, "Mages")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Monsters, "Monsters")]
        [TestCase(TableNameConstants.Set.Collection.Groups.NaturalArmor, "NaturalArmor")]
        [TestCase(TableNameConstants.Set.Collection.Groups.OverwrittenStrengths, "OverwrittenStrengths")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Spellcasters, "Spellcasters")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Standard, "Standard")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Stealth, "Stealth")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Warriors, "Warriors")]
        [TestCase(TableNameConstants.Set.Percentile.AlignmentGoodness, "AlignmentGoodness")]
        [TestCase(TableNameConstants.Set.Percentile.Traits, "Traits")]
        [TestCase(TableNameConstants.Formattable.Percentile.GOODNESSCharacterClasses, "{0}CharacterClasses")]
        [TestCase(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, "{0}{1}BaseRaces")]
        [TestCase(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, "{0}{1}Metaraces")]
        [TestCase(TableNameConstants.Formattable.Collection.BASERACESkillAdjustments, "{0}SkillAdjustments")]
        [TestCase(TableNameConstants.Formattable.Collection.CLASSFeatLevelRequirements, "{0}FeatLevelRequirements")]
        [TestCase(TableNameConstants.Formattable.Collection.FEATSkillAdjustments, "{0}SkillAdjustments")]
        [TestCase(TableNameConstants.Formattable.Collection.METARACESkillAdjustments, "{0}SkillAdjustments")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}