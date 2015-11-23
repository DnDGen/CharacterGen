using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Stress.Items
{
    [TestFixture]
    public class EquipmentGeneratorTests : StressTests
    {
        [Inject]
        public IEquipmentGenerator EquipmentGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject, Named(CombatGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("Equipment Generator")]
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

            var equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.Not.Null);
            Assert.That(equipment.PrimaryHand.Name, Is.Not.Empty);
            Assert.That(equipment.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(equipment.Treasure, Is.Not.Null);
            Assert.That(equipment.Treasure.Items, Is.Not.Empty);
        }
    }
}
