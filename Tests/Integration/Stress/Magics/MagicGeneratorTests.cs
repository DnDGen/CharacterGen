using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Magics
{
    [TestFixture]
    public class MagicGeneratorTests : StressTests
    {
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator CharacterAbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Heroic)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject, Named(CombatGeneratorTypeConstants.Character)]
        public ICombatGenerator CharacterCombatGenerator { get; set; }
        [Inject]
        public IMagicGenerator MagicGenerator { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.Spellcaster)]
        public override IClassNameRandomizer ClassNameRandomizer { get; set; }

        [TestCase("Magic Generator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = Generate(() => GetNewCharacterClass(alignment), c => c.Level > 3);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CharacterCombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = CharacterAbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var magic = MagicGenerator.GenerateWith(alignment, characterClass, race, ability.Stats, ability.Feats);
            Assert.That(magic, Is.Not.Null);
            Assert.That(magic.Animal, Is.Not.Null);
            Assert.That(magic.SpellsPerDay, Is.Not.Empty);
        }
    }
}
