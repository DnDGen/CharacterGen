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
        private Mock<INameSelector> mockNameSelector;

        [SetUp]
        public void Setup()
        {
            mockNameSelector = new Mock<INameSelector>();
            randomizer = new SetMetaraceRandomizer(mockNameSelector.Object);
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
            randomizer.SetMetaraceId = "metarace";
            mockNameSelector.Setup(s => s.Select("metarace")).Returns("Meta-race");

            var baseRace = randomizer.Randomize(String.Empty, characterclass);
            Assert.That(baseRace.Id, Is.EqualTo("metarace"));
            Assert.That(baseRace.Name, Is.EqualTo("Meta-race"));
        }

        [Test]
        public void ReturnJustSetMetarace()
        {
            randomizer.SetMetaraceId = "metarace";

            var baseRaces = randomizer.GetAllPossibleIds(String.Empty, characterclass);
            Assert.That(baseRaces, Contains.Item("metarace"));
            Assert.That(baseRaces.Count(), Is.EqualTo(1));
        }
    }
}