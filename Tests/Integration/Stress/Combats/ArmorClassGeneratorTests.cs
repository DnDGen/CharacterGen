using Ninject;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Items;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Combats
{
    [TestFixture]
    public class ArmorClassGeneratorTests : StressTests
    {
        [Inject]
        public IArmorClassGenerator ArmorClassGenerator { get; set; }
        [Inject]
        public IEquipmentGenerator EquipmentGenerator { get; set; }
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }

        private void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
            var equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
            var combat = CombatGenerator.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, equipment);

            var armorClass = ArmorClassGenerator.GenerateWith(equipment, combat.AdjustedDexterityBonus, ability.Feats, race);
            Assert.That(armorClass.FlatFooted, Is.AtLeast(10));
            Assert.That(armorClass.Full, Is.AtLeast(5));
            Assert.That(armorClass.Touch, Is.AtLeast(5));
        }
    }
}