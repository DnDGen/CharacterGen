﻿using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Factories
{
    [TestFixture]
    public class RaceFactoryTests : DurationTest
    {
        [Inject]
        public IRaceFactory RaceFactory { get; set; }
        [Inject]
        public IBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public IMetaraceRandomizer MetaraceRandomizer { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }
        [Inject]
        public CharacterClassPrototype CharacterClassPrototype { get; set; }

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
        public void RaceGeneration()
        {
            RaceFactory.CreateWith(Alignment.Goodness, CharacterClassPrototype, BaseRaceRandomizer, MetaraceRandomizer);
            AssertDuration();
        }
    }
}