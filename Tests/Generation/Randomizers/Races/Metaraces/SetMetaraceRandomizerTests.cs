using System;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests
    {
        [Test]
        public void ReturnSetMetarace()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = "metarace";

            var metarace = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(metarace, Is.EqualTo("metarace"));
        }
    }
}