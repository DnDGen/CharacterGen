﻿using DnDGen.CharacterGen.Randomizers.Abilities;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class AbilitiesRandomizerTypeConstantsTests
    {
        [TestCase(AbilitiesRandomizerTypeConstants.Average, "Average")]
        [TestCase(AbilitiesRandomizerTypeConstants.BestOfFour, "Best of four")]
        [TestCase(AbilitiesRandomizerTypeConstants.Good, "Good")]
        [TestCase(AbilitiesRandomizerTypeConstants.Heroic, "Heroic")]
        [TestCase(AbilitiesRandomizerTypeConstants.OnesAsSixes, "Ones as sixes")]
        [TestCase(AbilitiesRandomizerTypeConstants.Poor, "Poor")]
        [TestCase(AbilitiesRandomizerTypeConstants.Raw, "Raw")]
        [TestCase(AbilitiesRandomizerTypeConstants.TwoTenSidedDice, "2d10")]
        public void Constant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
