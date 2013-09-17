using NUnit.Framework;
using System;
using NPCGen.Core.Data.CharacterClasses;

namespace NPCGen.Tests.Data.Classes
{
    [TestFixture]
    public class BaseAttackTests
    {
        private BaseAttack baseAttack;

        [SetUp]
        public void Setup()
        {
            baseAttack = new BaseAttack();
        }

        [Test]
        public void OneAttack()
        {
            for (var a = 0; a < 6; a++)
                AssertOneAttack(a);
        }

        private void AssertOneAttack(Int32 baseAttackBonus)
        {
            baseAttack.BaseAttackBonus = baseAttackBonus;
            var expected = String.Format("+{0}", baseAttackBonus);
            Assert.That(baseAttack.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void TwoAttacks()
        {
            for (var a = 6; a < 11; a++)
                AssertTwoAttacks(a);
        }

        private void AssertTwoAttacks(Int32 baseAttackBonus)
        {
            baseAttack.BaseAttackBonus = baseAttackBonus;
            var expected = String.Format("+{0}/+{1}", baseAttackBonus, baseAttackBonus - 5);
            Assert.That(baseAttack.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void ThreeAttacks()
        {
            for (var a = 11; a < 16; a++)
                AssertThreeAttacks(a);
        }

        private void AssertThreeAttacks(Int32 baseAttackBonus)
        {
            baseAttack.BaseAttackBonus = baseAttackBonus;
            var expected = String.Format("+{0}/+{1}/+{2}", baseAttackBonus, baseAttackBonus - 5, baseAttackBonus - 10);
            Assert.That(baseAttack.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void FourAttacks()
        {
            for (var a = 16; a < 21; a++)
                AssertFourAttacks(a);
        }

        private void AssertFourAttacks(Int32 baseAttackBonus)
        {
            baseAttack.BaseAttackBonus = baseAttackBonus;
            var expected = String.Format("+{0}/+{1}/+{2}/+{3}", baseAttackBonus, baseAttackBonus - 5, baseAttackBonus - 10, baseAttackBonus - 15);
            Assert.That(baseAttack.ToString(), Is.EqualTo(expected));
        }
    }
}