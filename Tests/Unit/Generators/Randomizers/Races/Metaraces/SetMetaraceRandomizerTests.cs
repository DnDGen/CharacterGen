using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Domain.Randomizers.Races.Metaraces;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests
    {
        private ISetMetaraceRandomizer randomizer;
        private CharacterClass characterClass;
        private Alignment alignment;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private List<string> alignmentMetaraces;
        private List<string> classMetaraces;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            randomizer = new SetMetaraceRandomizer(mockCollectionsSelector.Object);
            characterClass = new CharacterClass();
            alignment = new Alignment();
            alignmentMetaraces = new List<string>();
            classMetaraces = new List<string>();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            characterClass.ClassName = "class name";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, alignment.Goodness)).Returns(alignmentMetaraces);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, characterClass.ClassName)).Returns(classMetaraces);
        }

        [Test]
        public void SetMetaraceRandomizerIsAMetaraceRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<RaceRandomizer>());
        }

        [Test]
        public void ReturnSetMetarace()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            alignmentMetaraces.Add("metarace");
            classMetaraces.Add("other metarace");
            classMetaraces.Add("metarace");

            var metarace = randomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo("metarace"));
        }

        [Test]
        public void ReturnJustSetMetarace()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            alignmentMetaraces.Add("metarace");
            classMetaraces.Add("other metarace");
            classMetaraces.Add("metarace");

            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Contains.Item("metarace"));
            Assert.That(metaraces.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ThrowExceptionIfBaseRaceDoesNotMatchAlignment()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            classMetaraces.Add("other metarace");
            classMetaraces.Add("metarace");

            Assert.That(() => randomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ThrowExceptionIfBaseRaceDoesNotMatchClassName()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            alignmentMetaraces.Add("metarace");
            classMetaraces.Add("other metarace");

            Assert.That(() => randomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ReturnEmptyIfBaseRaceDoesNotMatchAlignment()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            classMetaraces.Add("other metarace");
            classMetaraces.Add("metarace");

            var baseRaces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Is.Empty);
        }

        [Test]
        public void ReturnEmptyIfBaseRaceDoesNotMatchClassName()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            alignmentMetaraces.Add("metarace");
            classMetaraces.Add("other metarace");

            var baseRaces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Is.Empty);
        }
    }
}