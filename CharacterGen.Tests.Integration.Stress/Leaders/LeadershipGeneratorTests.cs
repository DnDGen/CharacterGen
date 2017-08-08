using CharacterGen.Abilities;
using CharacterGen.Characters;
using CharacterGen.Leaders;
using CharacterGen.Randomizers.Abilities;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Tests.Integration.Stress.Characters;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Leaders
{
    [TestFixture]
    public class LeadershipGeneratorTests : StressTests
    {
        [Inject]
        public ILeadershipGenerator LeadershipGenerator { get; set; }
        [Inject]
        public ICharacterGenerator CharacterGenerator { get; set; }
        [Inject, Named(AbilitiesRandomizerTypeConstants.Heroic)]
        public IAbilitiesRandomizer HeroicAbilitiesRandomizer { get; set; }
        [Inject, Named(LevelRandomizerTypeConstants.VeryHigh)]
        public ILevelRandomizer VeryHighLevelRandomizer { get; set; }
        [Inject]
        public Random Random { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyNPC)]
        public IClassNameRandomizer NPCClassNameRandomizer { get; set; }
        [Inject]
        public CharacterVerifier CharacterVerifier { get; set; }

        [Test]
        public void StressLeadership()
        {
            stressor.Stress(AssertLeadership);
        }

        protected void AssertLeadership()
        {
            var leadership = GenerateLeadership();
            Assert.That(leadership, Is.Not.Null);
            Assert.That(leadership.FollowerQuantities.Level1, Is.AtLeast(leadership.FollowerQuantities.Level2));
            Assert.That(leadership.FollowerQuantities.Level2, Is.InRange(leadership.FollowerQuantities.Level3, leadership.FollowerQuantities.Level1));
            Assert.That(leadership.FollowerQuantities.Level3, Is.InRange(leadership.FollowerQuantities.Level4, leadership.FollowerQuantities.Level2));
            Assert.That(leadership.FollowerQuantities.Level4, Is.InRange(leadership.FollowerQuantities.Level5, leadership.FollowerQuantities.Level3));
            Assert.That(leadership.FollowerQuantities.Level5, Is.InRange(leadership.FollowerQuantities.Level6, leadership.FollowerQuantities.Level4));
            Assert.That(leadership.FollowerQuantities.Level6, Is.AtMost(leadership.FollowerQuantities.Level5));
            Assert.That(leadership.FollowerQuantities.Level1, Is.Not.Negative);
            Assert.That(leadership.FollowerQuantities.Level2, Is.Not.Negative);
            Assert.That(leadership.FollowerQuantities.Level3, Is.Not.Negative);
            Assert.That(leadership.FollowerQuantities.Level4, Is.Not.Negative);
            Assert.That(leadership.FollowerQuantities.Level5, Is.Not.Negative);
            Assert.That(leadership.FollowerQuantities.Level6, Is.Not.Negative);
            Assert.That(leadership.LeadershipModifiers, Is.Not.Null);
        }

        [Test]
        public void FollowersHappen()
        {
            var leadership = stressor.GenerateOrFail(GenerateLeadership, l => l.FollowerQuantities.Level1 > 0);

            Assert.That(leadership, Is.Not.Null);
            Assert.That(leadership.FollowerQuantities.Level1, Is.GreaterThan(leadership.FollowerQuantities.Level2));
            Assert.That(leadership.FollowerQuantities.Level2, Is.InRange(leadership.FollowerQuantities.Level3, leadership.FollowerQuantities.Level1));
            Assert.That(leadership.FollowerQuantities.Level3, Is.InRange(leadership.FollowerQuantities.Level4, leadership.FollowerQuantities.Level2));
            Assert.That(leadership.FollowerQuantities.Level4, Is.InRange(leadership.FollowerQuantities.Level5, leadership.FollowerQuantities.Level3));
            Assert.That(leadership.FollowerQuantities.Level5, Is.InRange(leadership.FollowerQuantities.Level6, leadership.FollowerQuantities.Level4));
            Assert.That(leadership.FollowerQuantities.Level6, Is.InRange(0, leadership.FollowerQuantities.Level5));
            Assert.That(leadership.FollowerQuantities.Level1, Is.Positive);
            Assert.That(leadership.FollowerQuantities.Level2, Is.Not.Negative);
            Assert.That(leadership.FollowerQuantities.Level3, Is.Not.Negative);
            Assert.That(leadership.FollowerQuantities.Level4, Is.Not.Negative);
            Assert.That(leadership.FollowerQuantities.Level5, Is.Not.Negative);
            Assert.That(leadership.FollowerQuantities.Level6, Is.Not.Negative);
            Assert.That(leadership.LeadershipModifiers, Is.Not.Null);
        }

        private Leadership GenerateLeadership()
        {
            //INFO: Generating a high-level leader takes too long.  Instead, we wil generate the individual items.  We will ignore animals for now
            //var leader = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, VeryHighLevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, HeroicAbilitiesRandomizer);

            //return LeadershipGenerator.GenerateLeadership(leader.Class.Level, leader.Abilities[AbilityConstants.Charisma].Bonus, leader.Magic.Animal);

            var level = VeryHighLevelRandomizer.Randomize();
            var abilities = HeroicAbilitiesRandomizer.Randomize();

            return LeadershipGenerator.GenerateLeadership(level, abilities[AbilityConstants.Charisma].Bonus, string.Empty);
        }

        [Test]
        public void AllFollowersHappen()
        {
            var leadership = stressor.GenerateOrFail(GenerateLeadership, l => l.FollowerQuantities.Level6 > 0);

            Assert.That(leadership, Is.Not.Null);
            Assert.That(leadership.FollowerQuantities.Level1, Is.GreaterThan(leadership.FollowerQuantities.Level2));
            Assert.That(leadership.FollowerQuantities.Level2, Is.InRange(leadership.FollowerQuantities.Level3, leadership.FollowerQuantities.Level1));
            Assert.That(leadership.FollowerQuantities.Level3, Is.InRange(leadership.FollowerQuantities.Level4, leadership.FollowerQuantities.Level2));
            Assert.That(leadership.FollowerQuantities.Level4, Is.InRange(leadership.FollowerQuantities.Level5, leadership.FollowerQuantities.Level3));
            Assert.That(leadership.FollowerQuantities.Level5, Is.InRange(leadership.FollowerQuantities.Level6, leadership.FollowerQuantities.Level4));
            Assert.That(leadership.FollowerQuantities.Level6, Is.InRange(1, leadership.FollowerQuantities.Level5));
            Assert.That(leadership.FollowerQuantities.Level1, Is.Positive);
            Assert.That(leadership.FollowerQuantities.Level2, Is.Positive);
            Assert.That(leadership.FollowerQuantities.Level3, Is.Positive);
            Assert.That(leadership.FollowerQuantities.Level4, Is.Positive);
            Assert.That(leadership.FollowerQuantities.Level5, Is.Positive);
            Assert.That(leadership.FollowerQuantities.Level6, Is.Positive);
            Assert.That(leadership.LeadershipModifiers, Is.Not.Null);
        }

        [Test]
        public void StressCohort()
        {
            stressor.Stress(AssertCohort);
        }

        public void AssertCohort()
        {
            var leaderAlignment = GetNewAlignment();
            var leaderClass = GetNewCharacterClass(leaderAlignment);
            leaderClass.Level = Random.Next(6, 21);
            var cohortScore = Random.Next(3, 26);

            var cohort = LeadershipGenerator.GenerateCohort(cohortScore, leaderClass.Level, leaderAlignment.Full, leaderClass.Name);
            CharacterVerifier.AssertCharacter(cohort);
            Assert.That(cohort.Equipment.Treasure.Items, Is.Not.Empty);
            Assert.That(cohort.Class.Level, Is.InRange(1, leaderClass.Level - 2));
        }

        [Test]
        public void StressNPCCohort()
        {
            stressor.Stress(GeneratAndAssertNPCCohort);
        }

        public void GeneratAndAssertNPCCohort()
        {
            var leaderAlignment = GetNewAlignment();
            var leaderClass = NPCClassNameRandomizer.Randomize(leaderAlignment);
            var leaderLevel = Random.Next(6, 21);
            var cohortScore = Random.Next(3, 26);

            var cohort = LeadershipGenerator.GenerateCohort(cohortScore, leaderLevel, leaderAlignment.Full, leaderClass);
            CharacterVerifier.AssertCharacter(cohort);
            Assert.That(cohort.Class.Level, Is.InRange(1, leaderLevel - 2));
        }

        [Test]
        public void StressFollower()
        {
            stressor.Stress(GenerateAndAssertFollower);
        }

        public void GenerateAndAssertFollower()
        {
            var leaderAlignment = GetNewAlignment();
            var leaderClass = GetNewCharacterClass(leaderAlignment);
            var followerLevel = Random.Next(1, 7);

            var follower = LeadershipGenerator.GenerateFollower(followerLevel, leaderAlignment.Full, leaderClass.Name);
            CharacterVerifier.AssertCharacter(follower);
            Assert.That(follower.Equipment.Treasure.Items, Is.Not.Empty);
            Assert.That(follower.Class.Level, Is.InRange(1, followerLevel));
        }

        [Test]
        public void StressNPCFollower()
        {
            stressor.Stress(GenerateAndAssertNPCFollower);
        }

        public void GenerateAndAssertNPCFollower()
        {
            var leaderAlignment = GetNewAlignment();
            var leaderClass = NPCClassNameRandomizer.Randomize(leaderAlignment);
            var followerLevel = Random.Next(1, 7);

            var follower = LeadershipGenerator.GenerateFollower(followerLevel, leaderAlignment.Full, leaderClass);
            CharacterVerifier.AssertCharacter(follower);
            Assert.That(follower.Class.Level, Is.InRange(1, followerLevel));
        }
    }
}
