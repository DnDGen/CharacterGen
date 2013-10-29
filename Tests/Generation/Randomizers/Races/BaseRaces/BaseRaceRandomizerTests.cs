using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
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
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(baseRaces);

            var adjustments = new Dictionary<String, Int32>();
            foreach(var baseRace in baseRaces)
                adjustments.Add(baseRace, 0);

            mockLevelAdjustmentsProvider.Setup(p => p.GetLevelAdjustments()).Returns(adjustments);

            prototype = new CharacterClassPrototype();
            prototype.Level = 1;
        }

        protected override IEnumerable<String> GetResults()
        {
            return randomizer.GetAllPossibleResults(String.Empty, prototype);
        }
    }
}