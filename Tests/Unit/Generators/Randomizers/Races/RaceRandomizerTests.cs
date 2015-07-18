using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races
{
    [TestFixture]
    public abstract class RaceRandomizerTests
    {
        protected Mock<IPercentileSelector> mockPercentileResultSelector;
        protected Mock<IAdjustmentsSelector> mockAdjustmentsSelector;

        protected CharacterClass characterClass;
        protected Dictionary<String, Int32> adjustments;

        [SetUp]
        public void RaceRandomizerTestsSetup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();

            adjustments = new Dictionary<String, Int32>();
            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(adjustments);

            characterClass = new CharacterClass();
            characterClass.Level = 1;
        }
    }
}