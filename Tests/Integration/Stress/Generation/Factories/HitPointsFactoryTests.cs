using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Factories
{
    [TestFixture]
    public class HitPointsFactoryTests : StressTest
    {
        [Inject]
        public IHitPointsFactory HitPointsFactory { get; set; }

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
        public void HitPointsFactoryReturnsHitPointsGreaterThanZero()
        {
            while (TestShouldKeepRunning())
            {
                var characterClass = GetNewInstanceOf<CharacterClass>();
                var stats = GetNewInstanceOf<Dictionary<String, Stat>>();
                var race = GetNewInstanceOf<Race>();
                var hitPoints = HitPointsFactory.CreateWith(characterClass, stats[StatConstants.Constitution].Bonus, race);
                Assert.That(hitPoints, Is.GreaterThan(0));
            }

            AssertIterations();
        }
    }
}