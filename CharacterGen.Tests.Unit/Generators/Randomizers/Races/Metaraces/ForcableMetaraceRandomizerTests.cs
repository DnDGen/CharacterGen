using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Randomizers.Races.Metaraces;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class ForcableMetaraceRandomizerTests
    {
        private TestForcableMetaraceRandomizer forcableMetaraceRandomizer;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;

        private string firstMetarace = "first metarace";
        private string secondMetarace = "second metarace";
        private CharacterClass characterClass;
        private Dictionary<string, int> adjustments;
        private Alignment alignment;
        private List<string> alignmentRaces;
        private List<string> classRaces;
        private List<string> metaraces;

        [SetUp]
        public void Setup()
        {
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            forcableMetaraceRandomizer = new TestForcableMetaraceRandomizer(mockPercentileSelector.Object, mockAdjustmentsSelector.Object, mockCollectionsSelector.Object);

            adjustments = new Dictionary<string, int>();
            alignment = new Alignment();
            characterClass = new CharacterClass();
            alignmentRaces = new List<string>();
            classRaces = new List<string>();

            metaraces = new List<string> { firstMetarace, secondMetarace, RaceConstants.Metaraces.None };
            foreach (var metarace in metaraces)
                adjustments[metarace] = 0;

            alignment.Goodness = Guid.NewGuid().ToString();
            alignment.Lawfulness = Guid.NewGuid().ToString();
            characterClass.Level = 1;
            characterClass.Name = Guid.NewGuid().ToString();

            alignmentRaces.Add(firstMetarace);
            alignmentRaces.Add(secondMetarace);
            classRaces.Add(firstMetarace);
            classRaces.Add(secondMetarace);

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, alignment.Goodness, characterClass.Name);
            mockPercentileSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(metaraces);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(firstMetarace);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, It.IsAny<string>())).Returns((string table, string name) => adjustments[name]);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, alignment.Full)).Returns(alignmentRaces);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, characterClass.Name)).Returns(classRaces);


            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            metaraces.Clear();
            Assert.That(() => forcableMetaraceRandomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeReturnsMetaraceFromSelector()
        {
            var result = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(result, Is.EqualTo(firstMetarace));
        }

        [Test]
        public void RandomizeRerollsNoMetarace()
        {
            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(RaceConstants.Metaraces.None)
                .Returns(firstMetarace);

            forcableMetaraceRandomizer.ForceMetarace = true;

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(firstMetarace));
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeDoesNotRerollNoMetarace()
        {
            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(RaceConstants.Metaraces.None)
                .Returns(firstMetarace);

            forcableMetaraceRandomizer.ForceMetarace = false;

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(RaceConstants.Metaraces.None));
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RandomizeRerollsInvalidMetaraceBecauseNotAllowed()
        {
            forcableMetaraceRandomizer.ForbiddenMetarace = firstMetarace;

            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstMetarace)
                .Returns(secondMetarace);

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(secondMetarace));
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeRerollsInvalidMetaraceBecauseOfAdjustment()
        {
            adjustments[firstMetarace] = 1;

            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstMetarace)
                .Returns(secondMetarace);

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(secondMetarace));
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeRerollsInvalidMetaraceBecauseAlignmentDoesNotAllow()
        {
            alignmentRaces.Remove(firstMetarace);

            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstMetarace)
                .Returns(secondMetarace);

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(secondMetarace));
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeRerollsInvalidMetaraceBecauseClassDoesNotAllow()
        {
            classRaces.Remove(firstMetarace);

            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstMetarace)
                .Returns(secondMetarace);

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(secondMetarace));
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeReturnsNoMetarace()
        {
            classRaces.Remove(firstMetarace);

            mockPercentileSelector.Setup(p => p.SelectFrom(It.IsAny<string>())).Returns(firstMetarace);

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(RaceConstants.Metaraces.None));
        }

        [Test]
        public void RandomizeReturnsForcedMetarace()
        {
            classRaces.Remove(firstMetarace);
            forcableMetaraceRandomizer.ForceMetarace = true;

            mockPercentileSelector.Setup(p => p.SelectFrom(It.IsAny<string>())).Returns(firstMetarace);

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(secondMetarace));
        }

        [Test]
        public void GetAllPossibleResultsGetsNonEmptyResults()
        {
            var races = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);

            Assert.That(races, Contains.Item(firstMetarace));
            Assert.That(races, Contains.Item(secondMetarace));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            forcableMetaraceRandomizer.ForbiddenMetarace = firstMetarace;
            var results = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);

            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.All.Not.EqualTo(firstMetarace));
        }

        [Test]
        public void IfForceMetaraceIsTrueThenEmptyMetaraceIsNotAllowed()
        {
            forcableMetaraceRandomizer.ForceMetarace = true;
            var results = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Is.All.Not.EqualTo(RaceConstants.Metaraces.None));
        }

        [Test]
        public void IfForceMetaraceIsFalseThenEmptyMetaraceIsAllowed()
        {
            forcableMetaraceRandomizer.ForceMetarace = false;
            var results = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(RaceConstants.Metaraces.None));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutMetaracesWithTooHighLevelAdjustments()
        {
            adjustments[firstMetarace] = 1;

            var results = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.All.Not.EqualTo(firstMetarace));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutMetaracesNotAllowedByAlignment()
        {
            alignmentRaces.Remove(firstMetarace);

            var results = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.All.Not.EqualTo(firstMetarace));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutMetaracesNotAllowedByClass()
        {
            classRaces.Remove(firstMetarace);

            var results = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.All.Not.EqualTo(firstMetarace));
        }

        private class TestForcableMetaraceRandomizer : ForcableMetaraceBase
        {
            public string ForbiddenMetarace { get; set; }

            public TestForcableMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionSelector)
                : base(percentileResultSelector, levelAdjustmentsSelector, new ConfigurableIterationGenerator(2), collectionSelector)
            { }

            protected override bool MetaraceIsAllowed(string metarace)
            {
                return metarace != ForbiddenMetarace;
            }
        }
    }
}