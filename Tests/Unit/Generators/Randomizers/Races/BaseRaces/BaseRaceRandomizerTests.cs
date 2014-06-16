using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTests : RaceRandomizerTests
    {
        protected IBaseRaceRandomizer randomizer;

        private CharacterClassPrototype prototype;

        [SetUp]
        public void Setup()
        {
            var baseRaces = RaceConstants.BaseRaces.GetBaseRaces();
            mockPercentileResultSelector.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(baseRaces);

            var adjustments = new Dictionary<String, Int32>();
            foreach(var baseRace in baseRaces)
                adjustments.Add(baseRace, 0);

            mockLevelAdjustmentsSelector.Setup(p => p.GetLevelAdjustments()).Returns(adjustments);

            prototype = new CharacterClassPrototype();
            prototype.Level = 1;
        }

        protected override IEnumerable<String> GetResults()
        {
            return randomizer.GetAllPossibleResults(String.Empty, prototype);
        }
    }
}