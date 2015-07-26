using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Domain.Randomizers.Races.Metaraces;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
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