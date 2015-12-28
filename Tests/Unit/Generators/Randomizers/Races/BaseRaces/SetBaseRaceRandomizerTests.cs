using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests
    {
        private ISetBaseRaceRandomizer randomizer;
        private CharacterClass characterClass;
        private Alignment alignment;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private List<string> alignmentBaseRaces;
        private List<string> classBaseRaces;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            randomizer = new SetBaseRaceRandomizer(mockCollectionsSelector.Object);
            characterClass = new CharacterClass();
            alignment = new Alignment();
            alignmentBaseRaces = new List<string>();
            classBaseRaces = new List<string>();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            characterClass.ClassName = "class name";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, alignment.Goodness)).Returns(alignmentBaseRaces);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, characterClass.ClassName)).Returns(classBaseRaces);

        }

        [Test]
        public void SetBaseRaceRandomizerIsABaseRaceRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<RaceRandomizer>());
        }

        [Test]
        public void ReturnSetBaseRace()
        {
            randomizer.SetBaseRace = "base race";
            alignmentBaseRaces.Add("other base race");
            alignmentBaseRaces.Add("base race");
            classBaseRaces.Add("other base race");
            classBaseRaces.Add("base race");

            var baseRace = randomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void ReturnJustSetBaseRace()
        {
            randomizer.SetBaseRace = "base race";
            alignmentBaseRaces.Add("other base race");
            alignmentBaseRaces.Add("base race");
            classBaseRaces.Add("other base race");
            classBaseRaces.Add("base race");

            var baseRaces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Contains.Item("base race"));
            Assert.That(baseRaces.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ThrowExceptionIfBaseRaceDoesNotMatchAlignment()
        {
            randomizer.SetBaseRace = "base race";
            alignmentBaseRaces.Add("other base race");
            classBaseRaces.Add("other base race");
            classBaseRaces.Add("base race");

            Assert.That(() => randomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ThrowExceptionIfBaseRaceDoesNotMatchClassName()
        {
            randomizer.SetBaseRace = "base race";
            alignmentBaseRaces.Add("other base race");
            alignmentBaseRaces.Add("base race");
            classBaseRaces.Add("other base race");

            Assert.That(() => randomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ReturnEmptyIfBaseRaceDoesNotMatchAlignment()
        {
            randomizer.SetBaseRace = "base race";
            alignmentBaseRaces.Add("other base race");
            classBaseRaces.Add("other base race");
            classBaseRaces.Add("base race");

            var baseRaces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Is.Empty);
        }

        [Test]
        public void ReturnEmptyIfBaseRaceDoesNotMatchClassName()
        {
            randomizer.SetBaseRace = "base race";
            alignmentBaseRaces.Add("other base race");
            alignmentBaseRaces.Add("base race");
            classBaseRaces.Add("other base race");

            var baseRaces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Is.Empty);
        }
    }
}