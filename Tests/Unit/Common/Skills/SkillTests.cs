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
            Assert.That(skill.ArmorCheckPenalty, Is.False);
            Assert.That(skill.ClassSkill, Is.False);
            Assert.That(skill.Bonus, Is.EqualTo(0));
            Assert.That(skill.Ranks, Is.EqualTo(0));
            Assert.That(skill.TotalSkillBonus, Is.EqualTo(0));
        }

        [Test]
        public void TotalSkillBonusAddsBonusAndStatBonus()
        {
            skill.Bonus = 9260;
            skill.BaseStat.Value = 22;

            Assert.That(skill.TotalSkillBonus, Is.EqualTo(9266));
        }

        [Test]
        public void TotalSkillBonusAddsFullRanksIfClassSkill()
        {
            skill.Bonus = 9060;
            skill.BaseStat.Value = 22;
            skill.ClassSkill = true;
            skill.Ranks = 200;

            Assert.That(skill.TotalSkillBonus, Is.EqualTo(9266));
        }

        [Test]
        public void TotalSkillBonusAddsHalfRanksIfNotClassSkill()
        {
            skill.Bonus = 9060;
            skill.BaseStat.Value = 22;
            skill.ClassSkill = false;
            skill.Ranks = 400;

            Assert.That(skill.TotalSkillBonus, Is.EqualTo(9266));
        }

        [Test]
        public void HalfRanksDoNotIncreaseTotalSkillBonus()
        {
            skill.Bonus = 9260;
            skill.BaseStat.Value = 20;
            skill.ClassSkill = false;
            skill.Ranks = 3;

            Assert.That(skill.TotalSkillBonus, Is.EqualTo(9266));
        }
    }
}