using System;
using System.Collections.Generic;
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
        public void Setup()
        {
            var metaraces = RaceConstants.Metaraces.GetMetaraces();
            mockPercentileResultSelector.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(metaraces);

            var adjustments = new Dictionary<String, Int32>();
            foreach (var baseRace in metaraces)
                adjustments.Add(baseRace, 0);

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