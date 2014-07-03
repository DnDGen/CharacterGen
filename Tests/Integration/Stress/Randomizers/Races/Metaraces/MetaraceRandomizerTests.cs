using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class MetaraceRandomizerTests : StressTests
    {
        protected abstract IEnumerable<String> allowedMetaraces { get; }

        private IEnumerable<String> metaraces;

        [SetUp]
        public void Setup()
        {
            metaraces = allowedMetaraces;
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            var metarace = MetaraceRandomizer.Randomize(alignment.Goodness, characterClass);
            Assert.That(metaraces, Contains.Item(metarace), testType);
        }
    }
}