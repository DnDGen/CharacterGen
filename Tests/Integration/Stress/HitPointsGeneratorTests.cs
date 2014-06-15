using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class HitPointsGeneratorTests : StressTests
    {
        [Inject]
        public IHitPointsGenerator HitPointsFactory { get; set; }

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