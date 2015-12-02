using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Magics
{
    [TestFixture]
    public class SpellsGeneratorTests : StressTests
    {
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Heroic)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject]
        public ISpellsGenerator SpellsGenerator { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.Spellcaster)]
        public override IClassNameRandomizer ClassNameRandomizer { get; set; }

        [TestCase("Spells Generator")]
        public override void Stress(String stressSubject)
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

            var spells = SpellsGenerator.GenerateFrom(characterClass, ability.Stats);
            Assert.That(spells, Is.Not.Empty);

            foreach (var spell in spells)
            {
                Assert.That(spell.Level, Is.InRange(0, 9));
                Assert.That(spell.Quantity, Is.Positive);

                if (spell.Level > 0)
                    Assert.That(spell.HasDomainSpell, Is.EqualTo(characterClass.SpecialistFields.Any()));
            }

            var spellLevels = spells.Select(s => s.Level);
            Assert.That(spellLevels, Is.Unique);

            var maxSpellLevel = spellLevels.Max();
            var minSpellLevel = spellLevels.Min();
            Assert.That(minSpellLevel, Is.EqualTo(0).Or.EqualTo(1));
            Assert.That(spellLevels.Count(), Is.EqualTo(maxSpellLevel - minSpellLevel + 1));
        }
    }
}
