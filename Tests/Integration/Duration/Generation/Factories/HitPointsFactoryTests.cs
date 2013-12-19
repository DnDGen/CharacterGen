using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Factories
{
    [TestFixture]
    public class HitPointsFactoryTests : DurationTest
    {
        [Inject]
        public IHitPointsFactory HitPointsFactory { get; set; }
        [Inject]
        public CharacterClass CharacterClass { get; set; }
        [Inject]
        public Dictionary<String, Stat> Stats { get; set; }
        [Inject]
        public Race Race { get; set; }

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
        public void HitPointsGeneration()
        {
            HitPointsFactory.CreateWith(CharacterClass, Stats[StatConstants.Constitution].Bonus, Race);
            AssertDuration();
        }
    }
}