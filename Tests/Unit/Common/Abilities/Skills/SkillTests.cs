using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
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
            Assert.That(skill.ArmorCheckPenalty, Is.False);
            Assert.That(skill.ClassSkill, Is.False);
            Assert.That(skill.Bonus, Is.EqualTo(0));
            Assert.That(skill.Ranks, Is.EqualTo(0));
            Assert.That(skill.CircumstantialBonus, Is.False);
        }

        [Test]
        public void GetTotalSkillBonusThrowsExceptionIfNoStat()
        {
            Assert.That(() => skill.GetTotalSkillBonus(), Throws.Exception);
        }

        [Test]
        public void GetTotalSkillBonusAddsBonusAndStatBonus()
        {
            skill.BaseStat = new Stat { Value = 22 };
            skill.Bonus = 9260;

            Assert.That(skill.GetTotalSkillBonus(), Is.EqualTo(9266));
        }

        [Test]
        public void GetTotalSkillBonusAddsFullRanksIfClassSkill()
        {
            skill.BaseStat = new Stat { Value = 22 };
            skill.Bonus = 9060;
            skill.ClassSkill = true;
            skill.Ranks = 200;

            Assert.That(skill.GetTotalSkillBonus(), Is.EqualTo(9266));
        }

        [Test]
        public void GetTotalSkillBonusAddsHalfRanksIfNotClassSkill()
        {
            skill.BaseStat = new Stat { Value = 22 };
            skill.Bonus = 9060;
            skill.ClassSkill = false;
            skill.Ranks = 400;

            Assert.That(skill.GetTotalSkillBonus(), Is.EqualTo(9266));
        }

        [Test]
        public void HalfRanksDoNotIncreaseTotalSkillBonus()
        {
            skill.BaseStat = new Stat { Value = 20 };
            skill.Bonus = 9260;
            skill.ClassSkill = false;
            skill.Ranks = 3;

            Assert.That(skill.GetTotalSkillBonus(), Is.EqualTo(9266));
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