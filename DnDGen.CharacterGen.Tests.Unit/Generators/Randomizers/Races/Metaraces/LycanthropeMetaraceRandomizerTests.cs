﻿using DnDGen.CharacterGen.Generators.Randomizers.Races.Metaraces;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Randomizers.Races;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class LycanthropeMetaraceRandomizerTests : MetaraceRandomizerTestBase
    {
        protected override IEnumerable<string> metaraces
        {
            get
            {
                return new[]
                {
                    "genetic metarace",
                    "lycanthrope metarace",
                    "undead metarace",
                    RaceConstants.Metaraces.None,
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new LycanthropeMetaraceRandomizer(mockPercentileSelector.Object, mockCollectionSelector.Object);

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Lycanthrope))
                .Returns(new[] { "lycanthrope metarace" });
        }

        [Test]
        public void LycanthropeMetaracesAllowed()
        {
            (randomizer as IForcableMetaraceRandomizer).ForceMetarace = false;
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Is.EquivalentTo(new[] { "lycanthrope metarace", RaceConstants.Metaraces.None }));
        }

        [Test]
        public void OnlyLycanthropeMetaracesAllowed()
        {
            (randomizer as IForcableMetaraceRandomizer).ForceMetarace = true;
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Is.EquivalentTo(new[] { "lycanthrope metarace" }));
        }
    }
}