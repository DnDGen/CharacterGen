using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Magics
{
    [TestFixture]
    public class MagicGeneratorTests : StressTests
    {
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Heroic)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject]
        public IMagicGenerator MagicGenerator { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.Spellcaster)]
        public override IClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public IEquipmentGenerator EquipmentGenerator { get; set; }

        [TestCase("Magic Generator")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = Generate(() => GetNewCharacterClass(alignment), c => c.Level > 3);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
            var equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass, race);

            var magic = MagicGenerator.GenerateWith(alignment, characterClass, race, ability.Stats, ability.Feats, equipment);
            Assert.That(magic, Is.Not.Null);
            Assert.That(magic.Animal, Is.Not.Null);
            Assert.That(magic.SpellsPerDay, Is.Not.Empty);
            Assert.That(magic.ArcaneSpellFailure, Is.InRange(0, 100));
        }
    }
}
