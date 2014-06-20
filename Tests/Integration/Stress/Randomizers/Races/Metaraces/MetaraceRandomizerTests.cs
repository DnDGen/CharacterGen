using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class MetaraceRandomizerTests : StressTests
    {
        protected abstract IEnumerable<String> particularMetaraces { get; }

        private IEnumerable<String> metaraces;

        [SetUp]
        public void Setup()
        {
            metaraces = particularMetaraces;
        }

        protected override void MakeAssertions()
        {
            var data = GetNewDependentData();
            var metarace = MetaraceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
            Assert.That(metaraces, Contains.Item(metarace), type);
        }
    }
}