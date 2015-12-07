using CharacterGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Abilities.Skills
{
    [TestFixture]
    public class SkillTests
    {
        private Skill skill;

        [SetUp]
        public void Setup()
        {
            skill = new Skill();
        }

        [Test]
        public void SkillInitialized()
        {
            Assert.That(skill.BaseStat, Is.Null);
            Assert.That(skill.ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(skill.ClassSkill, Is.False);
            Assert.That(skill.Bonus, Is.EqualTo(0));
            Assert.That(skill.Ranks, Is.EqualTo(0));
            Assert.That(skill.CircumstantialBonus, Is.False);
        }

        [Test]
        public void EffectiveRanksIsRanksIfClassSkill()
        {
            skill.Ranks = 5;
            skill.ClassSkill = true;

            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
        }

        [Test]
        public void EffectiveRanksIsHalfIfCrossClassSkill()
        {
            skill.Ranks = 5;
            skill.ClassSkill = false;

            Assert.That(skill.EffectiveRanks, Is.EqualTo(2.5));
        }
    }
}