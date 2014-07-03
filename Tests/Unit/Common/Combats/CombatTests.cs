﻿using NPCGen.Common.Combats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Combats
{
    [TestFixture]
    public class CombatTests
    {
        private Combat combat;

        [SetUp]
        public void Setup()
        {
            combat = new Combat();
        }

        [Test]
        public void CombatInitialized()
        {
            Assert.That(combat.ArmorClass, Is.Not.Null);
            Assert.That(combat.BaseAttack, Is.Not.Null);
            Assert.That(combat.HitPoints, Is.EqualTo(0));
            Assert.That(combat.SavingThrows, Is.Not.Null);
        }
    }
}