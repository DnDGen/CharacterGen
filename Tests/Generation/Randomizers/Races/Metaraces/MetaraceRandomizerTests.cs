﻿using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class MetaraceRandomizerTests : RaceRandomizerTests
    {
        protected IMetaraceRandomizer randomizer;

        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            var metaraces = RaceConstants.Metaraces.GetMetaraces();
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(metaraces);

            var adjustments = new Dictionary<String, Int32>();
            foreach (var baseRace in metaraces)
                adjustments.Add(baseRace, 0);

            mockLevelAdjustmentsProvider.Setup(p => p.GetLevelAdjustments()).Returns(adjustments);

            characterClass = new CharacterClass();
            characterClass.Level = 1;
        }

        protected override IEnumerable<String> GetResults()
        {
            return randomizer.GetAllPossibleResults(String.Empty, characterClass);
        }
    }
}