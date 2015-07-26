using System;
using System.Linq;
using CharacterGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Abilities.Skills
{
    [TestFixture]
    public class SkillConstantsTests
    {
        [TestCase(SkillConstants.Appraise, "Appraise")]
        [TestCase(SkillConstants.Balance, "Balance")]
        [TestCase(SkillConstants.Bluff, "Bluff")]
        [TestCase(SkillConstants.Climb, "Climb")]
        [TestCase(SkillConstants.Concentration, "Concentration")]
        [TestCase(SkillConstants.DecipherScript, "Decipher Script")]
        [TestCase(SkillConstants.Diplomacy, "Diplomacy")]
        [TestCase(SkillConstants.DisableDevice, "Disable Device")]
        [TestCase(SkillConstants.Disguise, "Disguise")]
        [TestCase(SkillConstants.EscapeArtist, "Escape Artist")]
        [TestCase(SkillConstants.Forgery, "Forgery")]
        [TestCase(SkillConstants.GatherInformation, "Gather Information")]
        [TestCase(SkillConstants.HandleAnimal, "Handle Animal")]
        [TestCase(SkillConstants.Heal, "Heal")]
        [TestCase(SkillConstants.Hide, "Hide")]
        [TestCase(SkillConstants.Intimidate, "Intimidate")]
        [TestCase(SkillConstants.Jump, "Jump")]
        [TestCase(SkillConstants.KnowledgeArcana, "Knowledge (Arcana)")]
        [TestCase(SkillConstants.KnowledgeArchitectureAndEngineering, "Knowledge (Architecture and Engineering)")]
        [TestCase(SkillConstants.KnowledgeDungeoneering, "Knowledge (Dungeoneering)")]
        [TestCase(SkillConstants.KnowledgeGeography, "Knowledge (Geography)")]
        [TestCase(SkillConstants.KnowledgeHistory, "Knowledge (History)")]
        [TestCase(SkillConstants.KnowledgeLocal, "Knowledge (Local)")]
        [TestCase(SkillConstants.KnowledgeNature, "Knowledge (Nature)")]
        [TestCase(SkillConstants.KnowledgeNobilityAndRoyalty, "Knowledge (Nobility and Royalty)")]
        [TestCase(SkillConstants.KnowledgeReligion, "Knowledge (Religion)")]
        [TestCase(SkillConstants.KnowledgeThePlanes, "Knowledge (The Planes)")]
        [TestCase(SkillConstants.Listen, "Listen")]
        [TestCase(SkillConstants.MoveSilently, "Move Silently")]
        [TestCase(SkillConstants.OpenLock, "Open Lock")]
        [TestCase(SkillConstants.Perform, "Perform")]
        [TestCase(SkillConstants.Ride, "Ride")]
        [TestCase(SkillConstants.Search, "Search")]
        [TestCase(SkillConstants.SenseMotive, "Sense Motive")]
        [TestCase(SkillConstants.SleightOfHand, "Sleight of Hand")]
        [TestCase(SkillConstants.Spellcraft, "Spellcraft")]
        [TestCase(SkillConstants.Spot, "Spot")]
        [TestCase(SkillConstants.Survival, "Survival")]
        [TestCase(SkillConstants.Swim, "Swim")]
        [TestCase(SkillConstants.Tumble, "Tumble")]
        [TestCase(SkillConstants.UseMagicDevice, "Use Magic Device")]
        [TestCase(SkillConstants.UseRope, "Use Rope")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void AllSkills()
        {
            var skills = SkillConstants.GetSkills();

            Assert.That(skills, Contains.Item(SkillConstants.Appraise));
            Assert.That(skills, Contains.Item(SkillConstants.Balance));
            Assert.That(skills, Contains.Item(SkillConstants.Bluff));
            Assert.That(skills, Contains.Item(SkillConstants.Climb));
            Assert.That(skills, Contains.Item(SkillConstants.Concentration));
            Assert.That(skills, Contains.Item(SkillConstants.DecipherScript));
            Assert.That(skills, Contains.Item(SkillConstants.Diplomacy));
            Assert.That(skills, Contains.Item(SkillConstants.DisableDevice));
            Assert.That(skills, Contains.Item(SkillConstants.Disguise));
            Assert.That(skills, Contains.Item(SkillConstants.EscapeArtist));
            Assert.That(skills, Contains.Item(SkillConstants.Forgery));
            Assert.That(skills, Contains.Item(SkillConstants.GatherInformation));
            Assert.That(skills, Contains.Item(SkillConstants.HandleAnimal));
            Assert.That(skills, Contains.Item(SkillConstants.Heal));
            Assert.That(skills, Contains.Item(SkillConstants.Hide));
            Assert.That(skills, Contains.Item(SkillConstants.Intimidate));
            Assert.That(skills, Contains.Item(SkillConstants.Jump));
            Assert.That(skills, Contains.Item(SkillConstants.KnowledgeArcana));
            Assert.That(skills, Contains.Item(SkillConstants.KnowledgeArchitectureAndEngineering));
            Assert.That(skills, Contains.Item(SkillConstants.KnowledgeDungeoneering));
            Assert.That(skills, Contains.Item(SkillConstants.KnowledgeGeography));
            Assert.That(skills, Contains.Item(SkillConstants.KnowledgeHistory));
            Assert.That(skills, Contains.Item(SkillConstants.KnowledgeLocal));
            Assert.That(skills, Contains.Item(SkillConstants.KnowledgeNature));
            Assert.That(skills, Contains.Item(SkillConstants.KnowledgeNobilityAndRoyalty));
            Assert.That(skills, Contains.Item(SkillConstants.KnowledgeReligion));
            Assert.That(skills, Contains.Item(SkillConstants.KnowledgeThePlanes));
            Assert.That(skills, Contains.Item(SkillConstants.Listen));
            Assert.That(skills, Contains.Item(SkillConstants.MoveSilently));
            Assert.That(skills, Contains.Item(SkillConstants.OpenLock));
            Assert.That(skills, Contains.Item(SkillConstants.Perform));
            Assert.That(skills, Contains.Item(SkillConstants.Ride));
            Assert.That(skills, Contains.Item(SkillConstants.Search));
            Assert.That(skills, Contains.Item(SkillConstants.SenseMotive));
            Assert.That(skills, Contains.Item(SkillConstants.SleightOfHand));
            Assert.That(skills, Contains.Item(SkillConstants.Spellcraft));
            Assert.That(skills, Contains.Item(SkillConstants.Spot));
            Assert.That(skills, Contains.Item(SkillConstants.Survival));
            Assert.That(skills, Contains.Item(SkillConstants.Swim));
            Assert.That(skills, Contains.Item(SkillConstants.Tumble));
            Assert.That(skills, Contains.Item(SkillConstants.UseMagicDevice));
            Assert.That(skills, Contains.Item(SkillConstants.UseRope));
            Assert.That(skills.Count(), Is.EqualTo(42));
        }
    }
}