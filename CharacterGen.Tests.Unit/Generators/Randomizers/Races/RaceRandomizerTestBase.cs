using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races
{
    [TestFixture]
    public abstract class RaceRandomizerTestBase
    {
        internal Mock<IPercentileSelector> mockPercentileSelector;
        internal Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        internal Mock<ICollectionsSelector> mockCollectionSelector;
        internal Generator generator;
        protected Alignment alignment;
        protected CharacterClass characterClass;
        protected Dictionary<string, int> adjustments;

        [SetUp]
        public void RaceRandomizerTestBaseSetup()
        {
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionSelector = new Mock<ICollectionsSelector>();
            generator = new ConfigurableIterationGenerator();
            adjustments = new Dictionary<string, int>();
            characterClass = new CharacterClass();
            alignment = new Alignment();

            alignment.Goodness = Guid.NewGuid().ToString();
            alignment.Lawfulness = Guid.NewGuid().ToString();
            characterClass.Level = 1;
            characterClass.Name = Guid.NewGuid().ToString();

            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, It.IsAny<string>())).Returns((string table, string name) => adjustments[name]);
        }
    }
}