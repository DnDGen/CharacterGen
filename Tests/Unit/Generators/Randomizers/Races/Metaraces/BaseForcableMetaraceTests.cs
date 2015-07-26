﻿using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Domain.Randomizers.Races.Metaraces;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class BaseForcableMetaraceTests
    {
        private TestMetaraceRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;

        private String firstMetarace = "first metarace";
        private String secondMetarace = "second metarace";
        private CharacterClass characterClass;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            var metaraces = new[] { firstMetarace, secondMetarace, RaceConstants.Metaraces.None };
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(metaraces);
            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<String>())).Returns(firstMetarace);

            adjustments = new Dictionary<String, Int32>();
            foreach (var metarace in metaraces)
                adjustments.Add(metarace, 0);

            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(adjustments);

            characterClass = new CharacterClass();
            characterClass.Level = 1;

            randomizer = new TestMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromPercentileResultSelector()
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
        public void RandomizeReturnsMetaraceFromSelector()
        {
            var result = randomizer.Randomize(String.Empty, characterClass);
            Assert.That(result, Is.EqualTo(firstMetarace));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNameMetaraces()
        {
            characterClass.ClassName = "className";
            randomizer.Randomize("goodness", characterClass);
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, "goodness", characterClass.ClassName);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedMetaraceIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<String>())).Returns("invalid metarace")
                .Returns(firstMetarace);

            randomizer.Randomize(String.Empty, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromSelector()
        {
            randomizer.GetAllPossible(String.Empty, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsGetsNonEmptyResults()
        {
            var races = randomizer.GetAllPossible(String.Empty, characterClass);

            Assert.That(races, Contains.Item(firstMetarace));
            Assert.That(races, Contains.Item(secondMetarace));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.ClassName = "className";
            randomizer.GetAllPossible("goodness", characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom("goodnessclassNameMetaraces"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            randomizer.ForbiddenMetarace = firstMetarace;
            var results = randomizer.GetAllPossible(String.Empty, characterClass);

            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.Not.Contains(firstMetarace));
        }

        [Test]
        public void IfForceMetaraceIsTrueThenEmptyMetaraceIsNotAllowed()
        {
            randomizer.ForceMetarace = true;
            var results = randomizer.GetAllPossible(String.Empty, characterClass);
            Assert.That(results, Is.Not.Contains(RaceConstants.Metaraces.None));
        }

        [Test]
        public void IfForceMetaraceIsFalseThenEmptyMetaraceIsAllowed()
        {
            randomizer.ForceMetarace = false;
            var results = randomizer.GetAllPossible(String.Empty, characterClass);
            Assert.That(results, Contains.Item(RaceConstants.Metaraces.None));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutMetaracesWithTooHighLevelAdjustments()
        {
            adjustments[firstMetarace] = 1;

            var results = randomizer.GetAllPossible(String.Empty, characterClass);
            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.Not.Contains(firstMetarace));
        }

        private class TestMetaraceRandomizer : BaseForcableMetarace
        {
            public String ForbiddenMetarace { get; set; }

            public TestMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector)
                : base(percentileResultSelector, levelAdjustmentsSelector)
            { }

            protected override Boolean MetaraceIsAllowed(String metarace)
            {
                return metarace != ForbiddenMetarace;
            }
        }
    }
}