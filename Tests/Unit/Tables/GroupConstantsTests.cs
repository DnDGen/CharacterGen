using System;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Tables
{
    [TestFixture]
    public class GroupConstantsTests
    {
        [TestCase(GroupConstants.Additional, "Additional")]
        [TestCase(GroupConstants.AverageBaseAttack, "Average Base Attack")]
        [TestCase(GroupConstants.CharacterClasses, "CharacterClasses")]
        [TestCase(GroupConstants.CombatStyles, "CombatStyles")]
        [TestCase(GroupConstants.Deflection, "Deflection")]
        [TestCase(GroupConstants.Dodge, "Dodge")]
        [TestCase(GroupConstants.FighterBonusFeats, "FighterBonusFeats")]
        [TestCase(GroupConstants.FavoredEnemies, "Favored Enemies")]
        [TestCase(GroupConstants.Genetic, "Genetic")]
        [TestCase(GroupConstants.GoodBaseAttack, "Good Base Attack")]
        [TestCase(GroupConstants.HasClassRequirements, "HasClassRequirements")]
        [TestCase(GroupConstants.HasSkillRequirements, "HasSkillRequirements")]
        [TestCase(GroupConstants.HasStatRequirements, "HasStatRequirements")]
        [TestCase(GroupConstants.Healers, "Healers")]
        [TestCase(GroupConstants.Knowledge, "Knowledge")]
        [TestCase(GroupConstants.Lycanthrope, "Lycanthrope")]
        [TestCase(GroupConstants.Mages, "Mages")]
        [TestCase(GroupConstants.ManualCrossbows, "Manual Crossbows")]
        [TestCase(GroupConstants.Monsters, "Monsters")]
        [TestCase(GroupConstants.NaturalArmor, "NaturalArmor")]
        [TestCase(GroupConstants.Proficiency, "Proficiency")]
        [TestCase(GroupConstants.SchoolsOfMagic, "Schools of Magic")]
        [TestCase(GroupConstants.Size, "Size")]
        [TestCase(GroupConstants.SkillBonus, "SkillBonus")]
        [TestCase(GroupConstants.Skills, "Skills")]
        [TestCase(GroupConstants.Spellcasters, "Spellcasters")]
        [TestCase(GroupConstants.Standard, "Standard")]
        [TestCase(GroupConstants.Stealth, "Stealth")]
        [TestCase(GroupConstants.TakenMultipleTimes, "TakenMultipleTimes")]
        [TestCase(GroupConstants.Warriors, "Warriors")]
        [TestCase(GroupConstants.Weapons, "Weapons")]
        [TestCase(GroupConstants.WeaponsWithUnarmedAndGrapple, "Weapons with unarmed and grapple")]
        [TestCase(GroupConstants.WeaponsWithUnarmedAndGrappleAndRay, "Weapons with unarmed, grapple, and ray")]
        [TestCase(GroupConstants.WizardBonusFeats, "WizardBonusFeats")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
