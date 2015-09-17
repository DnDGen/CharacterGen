using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Stress.Items
{
    [TestFixture]
    public class ArmorGeneratorTests : StressTests
    {
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject, Named(ItemTypeConstants.Armor)]
        public GearGenerator ArmorGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("ArmorGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var armor = GetArmor();

            if (armor != null)
            {
                Assert.That(armor.Name, Is.Not.Empty);
                Assert.That(armor.ItemType, Is.EqualTo(ItemTypeConstants.Armor), armor.Name);
            }
        }

        private Item GetArmor()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            return ArmorGenerator.GenerateFrom(ability.Feats, characterClass, race);
        }

        [Test]
        public void ArmorDoesNotHappen()
        {
            var armor = Generate<Item>(GetArmor,
                a => a == null);

            Assert.That(armor, Is.Null);
        }

        [Test]
        public void ArmorHappens()
        {
            var armor = Generate<Item>(GetArmor,
                a => a != null && a.Attributes.Contains(AttributeConstants.Shield) == false);

            Assert.That(armor.Attributes, Is.Not.Contains(AttributeConstants.Shield));
        }

        [Test]
        public void ShieldHappens()
        {
            var armor = Generate<Item>(GetArmor,
                a => a != null && a.Attributes.Contains(AttributeConstants.Shield));

            Assert.That(armor.Attributes, Contains.Item(AttributeConstants.Shield));
        }

        [Test]
        public void MundaneArmorHappens()
        {
            var armor = Generate<Item>(GetArmor,
                a => a != null && a.IsMagical == false);

            Assert.That(armor.IsMagical, Is.False);
        }

        [Test]
        public void MagicalArmorHappens()
        {
            var armor = Generate<Item>(GetArmor,
                a => a != null && a.IsMagical);

            Assert.That(armor.IsMagical, Is.True);
        }

        [Test]
        public void UncursedArmorHappens()
        {
            var armor = Generate<Item>(GetArmor,
                a => a != null && String.IsNullOrEmpty(a.Magic.Curse));

            Assert.That(armor.Magic.Curse, Is.Empty);
        }

        [Test]
        public void CursedArmorHappens()
        {
            var armor = Generate<Item>(GetArmor,
                a => a != null && String.IsNullOrEmpty(a.Magic.Curse) == false);

            Assert.That(armor.Magic.Curse, Is.Not.Empty);
        }

        [Test]
        public void SpecificCursedArmorHappens()
        {
            var armor = Generate<Item>(GetArmor,
                a => a != null && String.IsNullOrEmpty(a.Magic.Curse) == false && a.Attributes.Contains(AttributeConstants.Specific));

            Assert.That(armor.Magic.Curse, Is.Not.Empty);
            Assert.That(armor.Magic.Curse, Contains.Item(AttributeConstants.Specific));
        }

        [Test]
        public void IntelligentArmorHappens()
        {
            var armor = Generate<Item>(GetArmor,
                a => a != null && a.Magic.Intelligence.Ego > 0);

            Assert.That(armor.Magic.Intelligence.Ego, Is.Positive);
        }

        [Test]
        public void NonIntelligentArmorHappens()
        {
            var armor = Generate<Item>(GetArmor,
                a => a != null && a.Magic.Intelligence.Ego == 0);

            Assert.That(armor.Magic.Intelligence.Ego, Is.EqualTo(0));
        }
    }
}
