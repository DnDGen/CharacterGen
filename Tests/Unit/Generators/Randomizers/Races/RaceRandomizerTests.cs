using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races
{
    [TestFixture]
    public abstract class RaceRandomizerTests
    {
        protected Mock<IPercentileSelector> mockPercentileResultSelector;
        protected Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        protected Alignment alignment;
        protected CharacterClass characterClass;
        protected Dictionary<String, Int32> adjustments;

        [SetUp]
        public void RaceRandomizerTestsSetup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            adjustments = new Dictionary<String, Int32>();
            characterClass = new CharacterClass();
            alignment = new Alignment();

            characterClass.Level = 1;

            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(adjustments);
        }
    }
}