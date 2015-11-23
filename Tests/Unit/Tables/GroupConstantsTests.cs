using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Tables
{
    [TestFixture]
    public class GroupConstantsTests
    {
        [TestCase(GroupConstants.Additional, "Additional")]
        [TestCase(GroupConstants.All, "All")]
        [TestCase(GroupConstants.AverageBaseAttack, "Average Base Attack")]
        [TestCase(GroupConstants.CharacterClasses, "CharacterClasses")]
        [TestCase(GroupConstants.Deflection, "Deflection")]
        [TestCase(GroupConstants.FighterBonusFeats, "FighterBonusFeats")]
        [TestCase(GroupConstants.FavoredEnemies, "Favored Enemies")]
        [TestCase(GroupConstants.Genetic, "Genetic")]
        [TestCase(GroupConstants.GoodBaseAttack, "Good Base Attack")]
        [TestCase(GroupConstants.HasClassRequirements, "HasClassRequirements")]
        [TestCase(GroupConstants.HasSkillRequirements, "HasSkillRequirements")]
        [TestCase(GroupConstants.HasStatRequirements, "HasStatRequirements")]
        [TestCase(GroupConstants.Healers, "Healers")]
        [TestCase(GroupConstants.Initiative, "Initiative")]
        [TestCase(GroupConstants.Intuitive, "Intuitive")]
        [TestCase(GroupConstants.Knowledge, "Knowledge")]
        [TestCase(GroupConstants.Lycanthrope, "Lycanthrope")]
        [TestCase(GroupConstants.Mages, "Mages")]
        [TestCase(GroupConstants.ManualCrossbows, "Manual Crossbows")]
        [TestCase(GroupConstants.Monsters, "Monsters")]
        [TestCase(GroupConstants.NaturalArmor, "NaturalArmor")]
        [TestCase(GroupConstants.NeedsAmmunition, "Needs Ammunition")]
        [TestCase(GroupConstants.Proficiency, "Proficiency")]
        [TestCase(GroupConstants.SavingThrows, "Saving Throws")]
        [TestCase(GroupConstants.SchoolsOfMagic, "Schools of Magic")]
        [TestCase(GroupConstants.SelfTaught, "Self-Taught")]
        [TestCase(GroupConstants.Size, "Size")]
        [TestCase(GroupConstants.Skills, "Skills")]
        [TestCase(GroupConstants.Spellcasters, "Spellcasters")]
        [TestCase(GroupConstants.Standard, "Standard")]
        [TestCase(GroupConstants.Stealth, "Stealth")]
        [TestCase(GroupConstants.TakenMultipleTimes, "TakenMultipleTimes")]
        [TestCase(GroupConstants.Trained, "Trained")]
        [TestCase(GroupConstants.TwoHanded, "Two-Handed")]
        [TestCase(GroupConstants.Undead, "Undead")]
        [TestCase(GroupConstants.Warriors, "Warriors")]
        [TestCase(GroupConstants.WizardBonusFeats, "WizardBonusFeats")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
