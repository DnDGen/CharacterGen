using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Tables
{
    [TestFixture]
    public class GroupConstantsTests
    {
        [TestCase(GroupConstants.AddMonsterHitDiceToPower, "Add Monster Hit Dice to Power")]
        [TestCase(GroupConstants.Additional, "Additional")]
        [TestCase(GroupConstants.All, "All")]
        [TestCase(GroupConstants.ArmorCheckPenalty, "Armor Check Penalty")]
        [TestCase(GroupConstants.AverageBaseAttack, "Average Base Attack")]
        [TestCase(GroupConstants.CharacterClasses, "Character Classes")]
        [TestCase(GroupConstants.Deflection, "Deflection")]
        [TestCase(GroupConstants.DodgeBonus, "Dodge Bonus")]
        [TestCase(GroupConstants.FighterBonusFeats, "Fighter Bonus Feats")]
        [TestCase(GroupConstants.FavoredEnemies, "Favored Enemies")]
        [TestCase(GroupConstants.Genetic, "Genetic")]
        [TestCase(GroupConstants.GoodBaseAttack, "Good Base Attack")]
        [TestCase(GroupConstants.HasClassRequirements, "Has Class Requirements")]
        [TestCase(GroupConstants.HasSkillRequirements, "Has Skill Requirements")]
        [TestCase(GroupConstants.HasStatRequirements, "Has Stat Requirements")]
        [TestCase(GroupConstants.Healers, "Healers")]
        [TestCase(GroupConstants.Initiative, "Initiative")]
        [TestCase(GroupConstants.Intuitive, "Intuitive")]
        [TestCase(GroupConstants.Knowledge, "Knowledge")]
        [TestCase(GroupConstants.Lycanthrope, "Lycanthrope")]
        [TestCase(GroupConstants.Mages, "Mages")]
        [TestCase(GroupConstants.ManualCrossbows, "Manual Crossbows")]
        [TestCase(GroupConstants.Monsters, "Monsters")]
        [TestCase(GroupConstants.NaturalArmor, "Natural Armor")]
        [TestCase(GroupConstants.NeedsAmmunition, "Needs Ammunition")]
        [TestCase(GroupConstants.NPCs, "NPCs")]
        [TestCase(GroupConstants.Players, "Players")]
        [TestCase(GroupConstants.PoorBaseAttack, "Poor Base Attack")]
        [TestCase(GroupConstants.Proficiency, "Proficiency")]
        [TestCase(GroupConstants.SavingThrows, "Saving Throws")]
        [TestCase(GroupConstants.SchoolsOfMagic, "Schools of Magic")]
        [TestCase(GroupConstants.SelfTaught, "Self-Taught")]
        [TestCase(GroupConstants.Size, "Size")]
        [TestCase(GroupConstants.Skills, "Skills")]
        [TestCase(GroupConstants.Spellcasters, "Spellcasters")]
        [TestCase(GroupConstants.Standard, "Standard")]
        [TestCase(GroupConstants.Stealth, "Stealth")]
        [TestCase(GroupConstants.TakenMultipleTimes, "Taken Multiple Times")]
        [TestCase(GroupConstants.Trained, "Trained")]
        [TestCase(GroupConstants.TwoHanded, "Two-Handed")]
        [TestCase(GroupConstants.Undead, "Undead")]
        [TestCase(GroupConstants.Warriors, "Warriors")]
        [TestCase(GroupConstants.WizardBonusFeats, "Wizard Bonus Feats")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
