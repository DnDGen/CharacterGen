using System;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NoMetaraceRandomizerTests
    {
        [Test]
        public void AlwaysReturnsEmptyString()
        {
            var randomizer = new NoMetaraceRandomizer();
            var metarace = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(metarace, Is.EqualTo(String.Empty));
        }
    }
}