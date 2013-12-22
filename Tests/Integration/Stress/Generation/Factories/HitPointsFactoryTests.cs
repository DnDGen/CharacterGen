using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Tests.Integration.Common;
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
                var data = GetNewInstanceOf<DependentDataCollection>();
                var stats = GetNewInstanceOf<Dictionary<String, Stat>>();
                var hitPoints = HitPointsFactory.CreateWith(data.CharacterClass, stats[StatConstants.Constitution].Bonus, data.Race);
                Assert.That(hitPoints, Is.GreaterThan(0));
            }

            AssertIterations();
        }
    }
}