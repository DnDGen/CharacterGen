using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NonGoodMetaraceRandomizerTests : StressTests
    {
        [Inject]
        public NonGoodMetaraceRandomizer MetaraceRandomizer { get; set; }

        private IEnumerable<String> metaraces;

        protected override IMetaraceRandomizer GetMetaraceRandomizer(IKernel kernel)
        {
            var randomizer = kernel.Get<NonGoodMetaraceRandomizer>();
            randomizer.AllowNoMetarace = false;
            return randomizer;
        }

        [SetUp]
        public void Setup()
        {
            metaraces = new[]
                {
                    RaceConstants.Metaraces.HalfDragon,
                    RaceConstants.Metaraces.HalfFiend,
                    RaceConstants.Metaraces.Wereboar,
                    RaceConstants.Metaraces.Wererat,
                    RaceConstants.Metaraces.Weretiger,
                    RaceConstants.Metaraces.Werewolf,
                    String.Empty
                };
        }

        [Test]
        public void NonGoodMetaraceRandomizerReturnsMetaraceOrEmpty()
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
        public void NonGoodMetaraceRandomizerReturnsMetarace()
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