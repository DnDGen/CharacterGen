using System;
using Ninject;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Combats
{
    [TestFixture]
    public class ArmorClassGeneratorTests : StressTests
    {
        [Inject]
        public IArmorClassGenerator ArmorClassGenerator { get; set; }
        [Inject]
        public ITreasureGenerator TreasureGenerator { get; set; }
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("ArmorClassGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
            var equipment = TreasureGenerator.GenerateWith(ability.Feats, characterClass);
            var combat = CombatGenerator.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, equipment);

            var armorClass = ArmorClassGenerator.GenerateWith(equipment, combat.AdjustedDexterityBonus, ability.Feats, race);
            Assert.That(armorClass.FlatFooted, Is.AtLeast(10));
            Assert.That(armorClass.Full, Is.AtLeast(5));
            Assert.That(armorClass.Touch, Is.AtLeast(5));
        }
    }
}