﻿using CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonStandardBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<String> baseRaceIds
        {
            get
            {
                return new[]
                {
                    "nonstandard base race"
                };
            }
        }

        private Mock<ICollectionsSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            randomizer = new NonStandardBaseRaceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object,
                mockCollectionsSelector.Object);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Standard))
                .Returns(new[] { "standard base race" });
        }

        [TestCase("nonstandard base race")]
        public void Allowed(String baseRace)
        {
            var baseRaces = randomizer.GetAllPossibles(String.Empty, characterClass);
            Assert.That(baseRaces, Contains.Item(baseRace));
        }

        [TestCase("standard base race")]
        public void NotAllowed(String baseRace)
        {
            var baseRaces = randomizer.GetAllPossibles(String.Empty, characterClass);
            Assert.That(baseRaces, Is.Not.Contains(baseRace));
        }
    }
}