using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Stress.Items
{
    [TestFixture]
    public class ArmorGeneratorTests : StressTests
    {
        [Inject]
        public IArmorGenerator ArmorGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject, Named(CombatGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.Warrior)]
        public override IClassNameRandomizer ClassNameRandomizer { get; set; }

        [TestCase("Armor Generator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var armor = ArmorGenerator.GenerateArmorFrom(ability.Feats, characterClass, race);
            Assert.That(armor, Is.Not.Null);
            Assert.That(armor.ItemType, Is.EqualTo(ItemTypeConstants.Armor));
            Assert.That(armor.Name, Is.Not.Empty);
            Assert.That(armor.Attributes, Is.All.Not.EqualTo(AttributeConstants.Shield));
        }

        [Test]
        public void StressShields()
        {
            Stress(MakeShieldAssertions);
        }

        private void MakeShieldAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var shield = ArmorGenerator.GenerateShieldFrom(ability.Feats, characterClass, race);
            Assert.That(shield, Is.Not.Null);
            Assert.That(shield.ItemType, Is.EqualTo(ItemTypeConstants.Armor));
            Assert.That(shield.Name, Is.Not.Empty);
            Assert.That(shield.Attributes, Contains.Item(AttributeConstants.Shield));
        }
    }
}
