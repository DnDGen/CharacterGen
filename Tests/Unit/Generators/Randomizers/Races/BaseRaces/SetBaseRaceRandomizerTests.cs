﻿using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests
    {
        private ISetBaseRaceRandomizer randomizer;
        private CharacterClass characterclass;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetBaseRaceRandomizer();
            characterclass = new CharacterClass();
        }

        [Test]
        public void SetBaseRaceRandomizerIsABaseRaceRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<IBaseRaceRandomizer>());
        }

        [Test]
        public void ReturnSetBaseRace()
        {
            randomizer.SetBaseRace = "baserace";

            var baseRace = randomizer.Randomize(String.Empty, characterclass);
            Assert.That(baseRace, Is.EqualTo("baserace"));
        }

        [Test]
        public void ReturnJustSetBaseRace()
        {
            randomizer.SetBaseRace = "baserace";

            var baseRaces = randomizer.GetAllPossibles(String.Empty, characterclass);
            Assert.That(baseRaces, Contains.Item("baserace"));
            Assert.That(baseRaces.Count(), Is.EqualTo(1));
        }
    }
}