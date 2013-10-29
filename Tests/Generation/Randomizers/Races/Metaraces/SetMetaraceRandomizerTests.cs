using System;
using System.Linq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests
    {
        private SetMetaraceRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = "metarace";
        }

        [Test]
        public void RandomizeReturnsSetMetarace()
        {
            var metarace = randomizer.Randomize(String.Empty, new CharacterClassPrototype());
            Assert.That(metarace, Is.EqualTo("metarace"));
        }

        [Test]
        public void GetAllPossibleResultsReturnsEnmuerableOfSetMetarace()
        {
            var metaraces = randomizer.GetAllPossibleResults(String.Empty, new CharacterClassPrototype());
            Assert.That(metaraces.First(), Is.EqualTo("metarace"));
            Assert.That(metaraces.Count(), Is.EqualTo(1));
        }
    }
}