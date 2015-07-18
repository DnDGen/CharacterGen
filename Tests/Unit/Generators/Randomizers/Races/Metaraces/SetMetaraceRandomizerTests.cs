using System;
using System.Linq;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests
    {
        private ISetMetaraceRandomizer randomizer;
        private CharacterClass characterclass;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetMetaraceRandomizer();
            characterclass = new CharacterClass();
        }

        [Test]
        public void SetMetaraceRandomizerIsAMetaraceRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<IMetaraceRandomizer>());
        }

        [Test]
        public void ReturnSetMetarace()
        {
            randomizer.SetMetarace = "metarace";

            var baseRace = randomizer.Randomize(String.Empty, characterclass);
            Assert.That(baseRace, Is.EqualTo("metarace"));
        }

        [Test]
        public void ReturnJustSetMetarace()
        {
            randomizer.SetMetarace = "metarace";

            var baseRaces = randomizer.GetAllPossible(String.Empty, characterclass);
            Assert.That(baseRaces, Contains.Item("metarace"));
            Assert.That(baseRaces.Count(), Is.EqualTo(1));
        }
    }
}