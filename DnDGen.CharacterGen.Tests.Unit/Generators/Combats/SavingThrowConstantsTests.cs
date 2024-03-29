﻿using DnDGen.CharacterGen.Combats;
using NUnit.Framework;
using System;

namespace DnDGen.CharacterGen.Tests.Unit.Common.Combats
{
    [TestFixture]
    public class SavingThrowConstantsTests
    {
        [TestCase(SavingThrowConstants.Fortitude, "Fortitude")]
        [TestCase(SavingThrowConstants.Reflex, "Reflex")]
        [TestCase(SavingThrowConstants.Will, "Will")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
