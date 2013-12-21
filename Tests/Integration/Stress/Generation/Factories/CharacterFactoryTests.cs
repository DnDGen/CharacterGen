using System;
using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryTests : StressTest
    {
        [Inject]
        public ICharacterFactory CharacterFactory { get; set; }
        [Inject]
        public RawStatsRandomizer StatsRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void CharacterFactoryReturnsACharacter()
        {
            while (TestShouldKeepRunning())
            {
                var character = CharacterFactory.CreateWith(GetAlignmentRandomizer(kernel), GetClassNameRandomizer(kernel), GetLevelRandomizer(kernel), GetBaseRaceRandomizer(kernel),
                    GetMetaraceRandomizer(kernel), StatsRandomizer);
                Assert.That(character, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithAlignment()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            while (TestShouldKeepRunning())
            {
                var character = CharacterFactory.CreateWith(GetAlignmentRandomizer(kernel), GetClassNameRandomizer(kernel), GetLevelRandomizer(kernel), GetBaseRaceRandomizer(kernel),
                    GetMetaraceRandomizer(kernel), StatsRandomizer);

                Assert.That(character.Alignment, Is.Not.Null);
                Assert.That(goodnesses.Contains(character.Alignment.Goodness), Is.True);
                Assert.That(lawfulnesses.Contains(character.Alignment.Lawfulness), Is.True);
            }

            AssertIterations();
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithCharacterClass()
        {
            var classes = CharacterClassConstants.GetClassNames();

            while (TestShouldKeepRunning())
            {
                var character = CharacterFactory.CreateWith(GetAlignmentRandomizer(kernel), GetClassNameRandomizer(kernel), GetLevelRandomizer(kernel), GetBaseRaceRandomizer(kernel),
                    GetMetaraceRandomizer(kernel), StatsRandomizer);

                Assert.That(character.Class, Is.Not.Null);
                Assert.That(classes.Contains(character.Class.ClassName), Is.True);
                Assert.That(character.Class.Level, Is.GreaterThan(0));
            }

            AssertIterations();
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithFeats()
        {
            while (TestShouldKeepRunning())
            {
                var character = CharacterFactory.CreateWith(GetAlignmentRandomizer(kernel), GetClassNameRandomizer(kernel), GetLevelRandomizer(kernel), GetBaseRaceRandomizer(kernel),
                    GetMetaraceRandomizer(kernel), StatsRandomizer);
                Assert.That(character.Feats, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithHitPointsGreaterThanZero()
        {
            while (TestShouldKeepRunning())
            {
                var character = CharacterFactory.CreateWith(GetAlignmentRandomizer(kernel), GetClassNameRandomizer(kernel), GetLevelRandomizer(kernel), GetBaseRaceRandomizer(kernel),
                    GetMetaraceRandomizer(kernel), StatsRandomizer);
                Assert.That(character.HitPoints, Is.GreaterThan(0));
            }

            AssertIterations();
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithLanguages()
        {
            while (TestShouldKeepRunning())
            {
                var character = CharacterFactory.CreateWith(GetAlignmentRandomizer(kernel), GetClassNameRandomizer(kernel), GetLevelRandomizer(kernel), GetBaseRaceRandomizer(kernel),
                    GetMetaraceRandomizer(kernel), StatsRandomizer);
                Assert.That(character.Languages, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithRace()
        {
            while (TestShouldKeepRunning())
            {
                var character = CharacterFactory.CreateWith(GetAlignmentRandomizer(kernel), GetClassNameRandomizer(kernel), GetLevelRandomizer(kernel), GetBaseRaceRandomizer(kernel),
                    GetMetaraceRandomizer(kernel), StatsRandomizer);
                Assert.That(character.Race, Is.Not.Null);
                Assert.That(character.Race.BaseRace, Is.Not.EqualTo(String.Empty));
            }

            AssertIterations();
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithSkills()
        {
            while (TestShouldKeepRunning())
            {
                var character = CharacterFactory.CreateWith(GetAlignmentRandomizer(kernel), GetClassNameRandomizer(kernel), GetLevelRandomizer(kernel), GetBaseRaceRandomizer(kernel),
                    GetMetaraceRandomizer(kernel), StatsRandomizer);
                Assert.That(character.Skills, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithStats()
        {
            var statNames = StatConstants.GetStats();

            while (TestShouldKeepRunning())
            {
                var character = CharacterFactory.CreateWith(GetAlignmentRandomizer(kernel), GetClassNameRandomizer(kernel), GetLevelRandomizer(kernel), GetBaseRaceRandomizer(kernel),
                    GetMetaraceRandomizer(kernel), StatsRandomizer);
                Assert.That(character.Stats, Is.Not.Null);

                foreach (var statName in statNames)
                {
                    Assert.That(character.Stats.ContainsKey(statName), Is.True);

                    var stat = character.Stats[statName];
                    Assert.That(stat, Is.Not.Null);
                    Assert.That(stat.Value, Is.GreaterThan(0));
                }
            }

            AssertIterations();
        }
    }
}