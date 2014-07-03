using System;
using System.Linq;
using NPCGen.Common.Combats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Combats
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
        public void BaseAttackInitialized()
        {
            Assert.That(baseAttack.Bonus, Is.EqualTo(0));
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6, 1)]
        [TestCase(7, 7, 2)]
        [TestCase(8, 8, 3)]
        [TestCase(9, 9, 4)]
        [TestCase(10, 10, 5)]
        [TestCase(11, 11, 6, 1)]
        [TestCase(12, 12, 7, 2)]
        [TestCase(13, 13, 8, 3)]
        [TestCase(14, 14, 9, 4)]
        [TestCase(15, 15, 10, 5)]
        [TestCase(16, 16, 11, 6, 1)]
        [TestCase(17, 17, 12, 7, 2)]
        [TestCase(18, 18, 13, 8, 3)]
        [TestCase(19, 19, 14, 9, 4)]
        [TestCase(20, 20, 15, 10, 5)]
        public void Bonuses(Int32 bonus, params Int32[] bonuses)
        {
            baseAttack.Bonus = bonus;
            var attacks = baseAttack.GetAllBonuses();

            foreach (var attack in bonuses)
                Assert.That(attacks, Contains.Item(attack));

            var extras = attacks.Except(bonuses);
            Assert.That(extras, Is.Empty);
        }
    }
}