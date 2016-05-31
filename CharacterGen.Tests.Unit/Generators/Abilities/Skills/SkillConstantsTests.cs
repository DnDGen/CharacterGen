using CharacterGen.Abilities.Skills;
using NUnit.Framework;
using System;

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
    }
}