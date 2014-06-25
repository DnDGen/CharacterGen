using NPCGen.Common.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Skills
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
            Assert.That(skill.BaseStat, Is.Not.Null);
            Assert.That(skill.CanLearn, Is.False);
            Assert.That(skill.ClassSkill, Is.False);
            Assert.That(skill.FeatBonus, Is.EqualTo(0));
            Assert.That(skill.Name, Is.Empty);
            Assert.That(skill.Ranks, Is.EqualTo(0));
        }
    }
}