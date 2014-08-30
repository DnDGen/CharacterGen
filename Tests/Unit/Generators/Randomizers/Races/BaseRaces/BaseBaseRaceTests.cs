using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class BaseBaseRaceTests
    {
        private TestBaseRaceRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;
        private Mock<IAdjustmentsSelector> mockLevelAdjustmentsSelector;

        private String firstBaseRace = "first base race";
        private String secondBaseRace = "second base race";
        private CharacterClass characterClass;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            var baseRaces = new[] { firstBaseRace, secondBaseRace, String.Empty };
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(baseRaces);
            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<String>())).Returns(firstBaseRace);

            adjustments = new Dictionary<String, Int32>();
            foreach (var baseRace in baseRaces)
                adjustments.Add(baseRace, 0);

            mockLevelAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockLevelAdjustmentsSelector.Setup(p => p.SelectFrom("LevelAdjustments")).Returns(adjustments);

            characterClass = new CharacterClass();
            characterClass.Level = 1;

            randomizer = new TestBaseRaceRandomizer(mockPercentileResultSelector.Object, mockLevelAdjustmentsSelector.Object);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize(String.Empty, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(Enumerable.Empty<String>());
            Assert.That(() => randomizer.Randomize(String.Empty, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeReturnsBaseRaceFromPercentileResultSelector()
        {
            var result = randomizer.Randomize(String.Empty, characterClass);
            Assert.That(result, Is.EqualTo(firstBaseRace));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.ClassName = "className";
            randomizer.Randomize("goodness", characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectFrom("goodnessclassNameBaseRaces"), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedBaseRaceIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<String>())).Returns("invalid base race")
                .Returns(firstBaseRace);

            randomizer.Randomize(String.Empty, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromSelector()
        {
            randomizer.GetAllPossibleResults(String.Empty, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutEmptyStrings()
        {
            var classNames = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(classNames, Contains.Item(firstBaseRace));
            Assert.That(classNames, Contains.Item(secondBaseRace));
            Assert.That(classNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.ClassName = "className";
            randomizer.GetAllPossibleResults("goodness", characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom("goodnessclassNameBaseRaces"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            randomizer.NotAllowedBaseRace = firstBaseRace;

            var results = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(results, Contains.Item(secondBaseRace));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutBaseRacesWithTooHighLevelAdjustments()
        {
            adjustments[firstBaseRace] = 1;

            var results = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(results, Contains.Item(secondBaseRace));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        private class TestBaseRaceRandomizer : BaseBaseRace
        {
            public String NotAllowedBaseRace { get; set; }

            public TestBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector)
                : base(percentileResultSelector, levelAdjustmentSelector) { }

            protected override Boolean BaseRaceIsAllowed(String baseRace)
            {
                return baseRace != NotAllowedBaseRace;
            }
        }
    }
}