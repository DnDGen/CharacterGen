using NPCGen.Common.Skills;
using NPCGen.Common.Stats;
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
            Assert.That(skill.BaseStat, Is.Null);
            Assert.That(skill.ArmorCheckPenalty, Is.False);
            Assert.That(skill.ClassSkill, Is.False);
            Assert.That(skill.Bonus, Is.EqualTo(0));
            Assert.That(skill.Ranks, Is.EqualTo(0));
            Assert.That(skill.TotalSkillBonus, Is.EqualTo(0));
        }

        [Test]
        public void TotalSkillBonusThrowsExceptionIfNoStat()
        {
            Assert.That(() => skill.TotalSkillBonus, Throws.Exception);
        }

        [Test]
        public void TotalSkillBonusAddsBonusAndStatBonus()
        {
            skill.BaseStat = new Stat { Value = 22 };
            skill.Bonus = 9260;

            Assert.That(skill.TotalSkillBonus, Is.EqualTo(9266));
        }

        [Test]
        public void TotalSkillBonusAddsFullRanksIfClassSkill()
        {
            skill.BaseStat = new Stat { Value = 22 };
            skill.Bonus = 9060;
            skill.ClassSkill = true;
            skill.Ranks = 200;

            Assert.That(skill.TotalSkillBonus, Is.EqualTo(9266));
        }

        [Test]
        public void TotalSkillBonusAddsHalfRanksIfNotClassSkill()
        {
            skill.BaseStat = new Stat { Value = 22 };
            skill.Bonus = 9060;
            skill.ClassSkill = false;
            skill.Ranks = 400;

            Assert.That(skill.TotalSkillBonus, Is.EqualTo(9266));
        }

        [Test]
        public void HalfRanksDoNotIncreaseTotalSkillBonus()
        {
            skill.BaseStat = new Stat { Value = 20 };
            skill.Bonus = 9260;
            skill.ClassSkill = false;
            skill.Ranks = 3;

            Assert.That(skill.TotalSkillBonus, Is.EqualTo(9266));
        }
    }
}