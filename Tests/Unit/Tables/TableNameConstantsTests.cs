using System;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Tables
{
    [TestFixture]
    public class TableNameConstantsTests
    {
        [TestCase(TableNameConstants.Set.Adjustments.ArmorBonuses, "ArmorBonuses")]
        [TestCase(TableNameConstants.Set.Adjustments.ArmorCheckPenalties, "ArmorCheckPenalties")]
        [TestCase(TableNameConstants.Set.Adjustments.ClassHitDice, "ClassHitDice")]
        [TestCase(TableNameConstants.Set.Adjustments.FeatArmorAdjustments, "FeatArmorAdjustments")]
        [TestCase(TableNameConstants.Set.Adjustments.FeatInitiativeBonuses, "FeatInitiativeBonuses")]
        [TestCase(TableNameConstants.Set.Adjustments.FighterFeatLevelRequirements, "FighterFeatLevelRequirements")]
        [TestCase(TableNameConstants.Set.Adjustments.LandSpeeds, "LandSpeeds")]
        [TestCase(TableNameConstants.Set.Adjustments.LevelAdjustments, "LevelAdjustments")]
        [TestCase(TableNameConstants.Set.Adjustments.MaxDexterityBonus, "MaxDexterityBonus")]
        [TestCase(TableNameConstants.Set.Adjustments.MonsterHitDice, "MonsterHitDice")]
        [TestCase(TableNameConstants.Set.Adjustments.ProhibitedFieldQuantities, "ProhibitedFieldQuantities")]
        [TestCase(TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments, "RacialBaseAttackAdjustments")]
        [TestCase(TableNameConstants.Set.Adjustments.RacialInitiativeBonuses, "RacialInitiativeBonuses")]
        [TestCase(TableNameConstants.Set.Adjustments.RacialNaturalArmorBonuses, "RacialNaturalArmorBonuses")]
        [TestCase(TableNameConstants.Set.Adjustments.SkillPointsForClasses, "SkillPointsForClasses")]
        [TestCase(TableNameConstants.Set.Adjustments.SpecialistFieldQuantities, "SpecialistFieldQuantities")]
        [TestCase(TableNameConstants.Set.Collection.AdditionalFeatClassNameRequirements, "AdditionalFeatClassNameRequirements")]
        [TestCase(TableNameConstants.Set.Collection.AdditionalFeatData, "AdditionalFeatData")]
        [TestCase(TableNameConstants.Set.Collection.AdditionalFeatFeatRequirements, "AdditionalFeatFeatRequirements")]
        [TestCase(TableNameConstants.Set.Collection.ArmorClassModifiers, "ArmorClassModifiers")]
        [TestCase(TableNameConstants.Set.Collection.AutomaticLanguages, "AutomaticLanguages")]
        [TestCase(TableNameConstants.Set.Collection.BaseRaceGroups, "BaseRaceGroups")]
        [TestCase(TableNameConstants.Set.Collection.BonusLanguages, "BonusLanguages")]
        [TestCase(TableNameConstants.Set.Collection.ClassNameGroups, "ClassNameGroups")]
        [TestCase(TableNameConstants.Set.Collection.ClassSkills, "ClassSkills")]
        [TestCase(TableNameConstants.Set.Collection.CrossClassSkills, "CrossClassSkills")]
        [TestCase(TableNameConstants.Set.Collection.DragonSpecies, "DragonSpecies")]
        [TestCase(TableNameConstants.Set.Collection.FeatGroups, "FeatGroups")]
        [TestCase(TableNameConstants.Set.Collection.FeatFoci, "FeatFoci")]
        [TestCase(TableNameConstants.Set.Collection.MetaraceGroups, "MetaraceGroups")]
        [TestCase(TableNameConstants.Set.Collection.Names, "Names")]
        [TestCase(TableNameConstants.Set.Collection.ProhibitedFields, "ProhibitedFields")]
        [TestCase(TableNameConstants.Set.Collection.RacialFeatHitDieRequirements, "RacialFeatHitDieRequirements")]
        [TestCase(TableNameConstants.Set.Collection.SkillData, "SkillData")]
        [TestCase(TableNameConstants.Set.Collection.SkillGroups, "SkillGroups")]
        [TestCase(TableNameConstants.Set.Collection.SkillSynergy, "SkillSynergy")]
        [TestCase(TableNameConstants.Set.Collection.SkillSynergyFeats, "SkillSynergyFeats")]
        [TestCase(TableNameConstants.Set.Collection.SpecialistFields, "SpecialistFields")]
        [TestCase(TableNameConstants.Set.Collection.StatPriorities, "StatPriorities")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Additional, "Additional")]
        [TestCase(TableNameConstants.Set.Collection.Groups.AverageBaseAttack, "Average Base Attack")]
        [TestCase(TableNameConstants.Set.Collection.Groups.CharacterClasses, "CharacterClasses")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Deflection, "Deflection")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Dodge, "Dodge")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Genetic, "Genetic")]
        [TestCase(TableNameConstants.Set.Collection.Groups.GoodBaseAttack, "Good Base Attack")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Healers, "Healers")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Knowledge, "Knowledge")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Lycanthrope, "Lycanthrope")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Mages, "Mages")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Monsters, "Monsters")]
        [TestCase(TableNameConstants.Set.Collection.Groups.NaturalArmor, "NaturalArmor")]
        [TestCase(TableNameConstants.Set.Collection.Groups.SchoolsOfMagic, "Schools of Magic")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Size, "Size")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Skills, "Skills")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Spellcasters, "Spellcasters")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Standard, "Standard")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Stealth, "Stealth")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Warriors, "Warriors")]
        [TestCase(TableNameConstants.Set.Collection.Groups.Weapons, "Weapons")]
        [TestCase(TableNameConstants.Set.Collection.Groups.WeaponsWithUnarmedAndGrapple, "Weapons with unarmed and grapple")]
        [TestCase(TableNameConstants.Set.Collection.Groups.WeaponsWithUnarmedAndGrappleAndRay, "Weapons with unarmed, grapple, and ray")]
        [TestCase(TableNameConstants.Set.Percentile.AlignmentGoodness, "AlignmentGoodness")]
        [TestCase(TableNameConstants.Set.Percentile.Traits, "Traits")]
        [TestCase(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill, "AssignPointToCrossClassSkill")]
        [TestCase(TableNameConstants.Formattable.Adjustments.CLASSFeatLevelRequirements, "{0}FeatLevelRequirements")]
        [TestCase(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, "{0}SkillRankRequirements")]
        [TestCase(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, "{0}StatRequirements")]
        [TestCase(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, "{0}StatAdjustments")]
        [TestCase(TableNameConstants.Formattable.Percentile.GOODNESSCharacterClasses, "{0}CharacterClasses")]
        [TestCase(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, "{0}{1}BaseRaces")]
        [TestCase(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, "{0}{1}Metaraces")]
        [TestCase(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, "{0}HasSpecialistFields")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void BaseRaceSkillAdjustmentsTable()
        {
            Assert.That(TableNameConstants.Formattable.Adjustments.BASERACESkillAdjustments, Is.EqualTo("{0}SkillAdjustments"));
        }

        [Test]
        public void FeatSkillAdjustmentsTable()
        {
            Assert.That(TableNameConstants.Formattable.Adjustments.FEATSkillAdjustments, Is.EqualTo("{0}SkillAdjustments"));
        }

        [Test]
        public void MetaraceSkillAdjustmentsTable()
        {
            Assert.That(TableNameConstants.Formattable.Adjustments.METARACESkillAdjustments, Is.EqualTo("{0}SkillAdjustments"));
        }

        [Test]
        public void RaceFeatDataTable()
        {
            Assert.That(TableNameConstants.Formattable.Collection.RACEFeatData, Is.EqualTo("{0}FeatData"));
        }

        [Test]
        public void ClassFeatDataTable()
        {
            Assert.That(TableNameConstants.Formattable.Collection.CLASSFeatData, Is.EqualTo("{0}FeatData"));
        }
    }
}