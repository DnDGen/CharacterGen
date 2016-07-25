using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Abilities.Skills
{
    [TestFixture]
    public class SkillTests
    {
        private Skill skill;
        private Stat baseStat;

        [SetUp]
        public void Setup()
        {
            baseStat = new Stat("base stat");
            skill = new Skill("skill name", baseStat, 90210);
        }

        [Test]
        public void SkillInitialized()
        {
            Assert.That(skill.Name, Is.EqualTo("skill name"));
            Assert.That(skill.BaseStat, Is.EqualTo(baseStat));
            Assert.That(skill.ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(skill.ClassSkill, Is.False);
            Assert.That(skill.Bonus, Is.EqualTo(0));
            Assert.That(skill.Ranks, Is.EqualTo(0));
            Assert.That(skill.CircumstantialBonus, Is.False);
            Assert.That(skill.RankCap, Is.EqualTo(90210));
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

        [Test]
        public void RanksMaxedOutIfEqualToRankCap()
        {
            skill.Ranks = skill.RankCap;
            Assert.That(skill.RanksMaxedOut, Is.True);
        }

        [Test]
        public void RanksNotMaxedOutIfLessThanRankCap()
        {
            skill.Ranks = skill.RankCap - 1;
            Assert.That(skill.RanksMaxedOut, Is.False);
        }

        [Test]
        public void AdjustRankCap()
        {
            skill.RankCap += 600;
            Assert.That(skill.RankCap, Is.EqualTo(90810));
        }

        [Test]
        public void SetRanks()
        {
            skill.Ranks = 9266;
            Assert.That(skill.Ranks, Is.EqualTo(9266));
        }

        [Test]
        public void CannotSetRanksAboveRankCap()
        {
            skill.Ranks = skill.RankCap;
            Assert.That(() => skill.Ranks++, Throws.InvalidOperationException.With.Message.EqualTo("Ranks cannot exceed the Rank Cap"));
        }

        [Test]
        public void ClassSkillQualifiesForSkillSynergy()
        {
            skill.Ranks = 5;
            skill.ClassSkill = true;

            Assert.That(skill.QualifiesForSkillSynergy, Is.True);
        }

        [Test]
        public void ClassSkillDoesNotQualifyForSkillSynergy()
        {
            skill.Ranks = 4;
            skill.ClassSkill = true;

            Assert.That(skill.QualifiesForSkillSynergy, Is.False);
        }

        [Test]
        public void CrossClassSkillQualifiesForSkillSynergy()
        {
            skill.Ranks = 10;
            skill.ClassSkill = false;

            Assert.That(skill.QualifiesForSkillSynergy, Is.True);
        }

        [Test]
        public void CrossClassSkillDoesNotQualifyForSkillSynergy()
        {
            skill.Ranks = 9;
            skill.ClassSkill = false;

            Assert.That(skill.QualifiesForSkillSynergy, Is.False);
        }
    }
}