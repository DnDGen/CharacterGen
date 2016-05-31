using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races
{
    [TestFixture]
    public abstract class RaceRandomizerTests
    {
        internal Mock<IPercentileSelector> mockPercentileResultSelector;
        internal Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        internal Generator generator;
        protected Alignment alignment;
        protected CharacterClass characterClass;
        protected Dictionary<string, int> adjustments;

        [SetUp]
        public void RaceRandomizerTestsSetup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            generator = new ConfigurableIterationGenerator();
            adjustments = new Dictionary<string, int>();
            characterClass = new CharacterClass();
            alignment = new Alignment();

            characterClass.Level = 1;

            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(adjustments);
        }
    }
}