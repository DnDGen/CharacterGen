using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NonEvilMetaraceRandomizerTests : StressTest
    {
        [Inject]
        public NonEvilMetaraceRandomizer MetaraceRandomizer { get; set; }

        private IEnumerable<String> metaraces;

        protected override IMetaraceRandomizer GetMetaraceRandomizer(IKernel kernel)
        {
            var randomizer = kernel.Get<NonEvilMetaraceRandomizer>();
            randomizer.AllowNoMetarace = false;
            return randomizer;
        }

        [SetUp]
        public void Setup()
        {
            metaraces = new[]
                {
                    RaceConstants.Metaraces.HalfDragon,
                    RaceConstants.Metaraces.HalfCelestial,
                    RaceConstants.Metaraces.Werebear,
                    RaceConstants.Metaraces.Wereboar,
                    RaceConstants.Metaraces.Weretiger,
                    String.Empty
                };

            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void NonEvilMetaraceRandomizerReturnsMetaraceOrEmpty()
        {
            MetaraceRandomizer.AllowNoMetarace = true;

            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var metarace = MetaraceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
                Assert.That(metaraces.Contains(metarace), Is.True);
            }

            AssertIterations();
        }

        [Test]
        public void NonEvilMetaraceRandomizerReturnsMetarace()
        {
            MetaraceRandomizer.AllowNoMetarace = false;

            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var metarace = MetaraceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
                Assert.That(metarace, Is.Not.Empty);
                Assert.That(metaraces.Contains(metarace), Is.True);
            }

            AssertIterations();
        }
    }
}