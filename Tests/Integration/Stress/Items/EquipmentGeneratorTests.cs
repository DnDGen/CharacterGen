using CharacterGen.Common.Items;
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
    public class TreasureGeneratorTests : StressTests
    {
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public IEquipmentGenerator EquipmentGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("EquipmentGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var equipment = GetEquipment();
            Assert.That(equipment.Treasure, Is.Not.Null);
            Assert.That(equipment.Treasure.Items, Is.All.Not.Null, String.Join(",", equipment.Treasure.Items));
            Assert.That(equipment.PrimaryHand.Name, Is.Not.Empty);
            Assert.That(equipment.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
        }

        private Equipment GetEquipment()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            return EquipmentGenerator.GenerateWith(ability.Feats, characterClass, race);
        }
    }
}