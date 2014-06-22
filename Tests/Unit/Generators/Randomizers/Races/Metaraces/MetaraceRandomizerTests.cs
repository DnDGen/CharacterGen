using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class MetaraceRandomizerTests : RaceRandomizerTests
    {
        protected IMetaraceRandomizer randomizer;

        private CharacterClassPrototype characterClass;

        [SetUp]
        public void MetaraceRandomizerTestsSetup()
        {
            var metaraces = RaceConstants.Metaraces.GetMetaraces().Union(new[] { String.Empty });
            mockPercentileResultSelector.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(metaraces);

            var adjustments = new Dictionary<String, Int32>();
            foreach (var metarace in metaraces)
                adjustments.Add(metarace, 0);

            mockLevelAdjustmentsSelector.Setup(p => p.GetAdjustments()).Returns(adjustments);

            characterClass = new CharacterClassPrototype();
            characterClass.Level = 1;
        }

        protected override IEnumerable<String> GetResults()
        {
            return randomizer.GetAllPossibleResults(String.Empty, characterClass);
        }
    }
}