using System;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Abilities.Skills
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

        [TestCase(1, 4)]
        [TestCase(2, 5)]
        [TestCase(3, 6)]
        [TestCase(4, 7)]
        [TestCase(5, 8)]
        [TestCase(6, 9)]
        [TestCase(7, 10)]
        [TestCase(8, 11)]
        [TestCase(9, 12)]
        [TestCase(10, 13)]
        [TestCase(11, 14)]
        [TestCase(12, 15)]
        [TestCase(13, 16)]
        [TestCase(14, 17)]
        [TestCase(15, 18)]
        [TestCase(16, 19)]
        [TestCase(17, 20)]
        [TestCase(18, 21)]
        [TestCase(19, 22)]
        [TestCase(20, 23)]
        public void MaxedOutClassSkill(Int32 level, Int32 maxRanks)
        {
            skill.BaseStat = new Stat { Value = 10 };
            skill.ClassSkill = true;

            skill.Ranks = maxRanks;
            var maxedOut = skill.IsMaxedOutFor(level);
            Assert.That(maxedOut, Is.True);
            Assert.That(skill.GetTotalSkillBonus(), Is.EqualTo(maxRanks));

            for (var ranks = 0; ranks < maxRanks; ranks++)
            {
                skill.Ranks = ranks;
                maxedOut = skill.IsMaxedOutFor(level);
                Assert.That(maxedOut, Is.False);
                Assert.That(skill.GetTotalSkillBonus(), Is.EqualTo(ranks));
            }
        }

        [TestCase(1, 4)]
        [TestCase(2, 5)]
        [TestCase(3, 6)]
        [TestCase(4, 7)]
        [TestCase(5, 8)]
        [TestCase(6, 9)]
        [TestCase(7, 10)]
        [TestCase(8, 11)]
        [TestCase(9, 12)]
        [TestCase(10, 13)]
        [TestCase(11, 14)]
        [TestCase(12, 15)]
        [TestCase(13, 16)]
        [TestCase(14, 17)]
        [TestCase(15, 18)]
        [TestCase(16, 19)]
        [TestCase(17, 20)]
        [TestCase(18, 21)]
        [TestCase(19, 22)]
        [TestCase(20, 23)]
        public void MaxedOutNonClassSkill(Int32 level, Int32 maxRanks)
        {
            skill.BaseStat = new Stat { Value = 10 };
            skill.ClassSkill = false;

            skill.Ranks = maxRanks;
            var maxedOut = skill.IsMaxedOutFor(level);
            Assert.That(maxedOut, Is.True);

            var totalSkillBonus = maxRanks / 2;
            Assert.That(skill.GetTotalSkillBonus(), Is.EqualTo(totalSkillBonus));

            for (var ranks = 0; ranks < maxRanks; ranks++)
            {
                skill.Ranks = ranks;
                maxedOut = skill.IsMaxedOutFor(level);
                Assert.That(maxedOut, Is.False);

                totalSkillBonus = ranks / 2;
                Assert.That(skill.GetTotalSkillBonus(), Is.EqualTo(totalSkillBonus));
            }
        }
    }
}