using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Randomizers.Stats;
using CharacterGen.Verifiers.Exceptions;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress
{
    [TestFixture]
    public class CharacterGeneratorTests : StressTests
    {
        [Inject]
        public ICharacterGenerator CharacterGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer RawStatsRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyNPC)]
        public IClassNameRandomizer NPCClassNameRandomizer { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Heroic)]
        public IStatsRandomizer HeroicStatsRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.Spellcaster)]
        public IClassNameRandomizer SpellcasterClassNameRandomizer { get; set; }
        [Inject]
        public ISetClassNameRandomizer SetClassNameRandomizer { get; set; }
        [Inject]
        public ISetLevelRandomizer SetLevelRandomizer { get; set; }
        [Inject]
        public CharacterVerifier CharacterVerifier { get; set; }

        [Test]
        public void StressCharacter()
        {
            Stress(AssertCharacter);
        }

        protected void AssertCharacter()
        {
            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(character);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty);
        }

        [Test]
        public void StressNPC()
        {
            Stress(AssertNPC);
        }

        private void AssertNPC()
        {
            var npc = CharacterGenerator.GenerateWith(AlignmentRandomizer, NPCClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(npc);
        }

        [Test]
        public void StressCommoner()
        {
            Stress(AssertCommoner);
        }

        private void AssertCommoner()
        {
            SetClassNameRandomizer.SetClassName = CharacterClassConstants.Commoner;

            var commoner = CharacterGenerator.GenerateWith(AlignmentRandomizer, SetClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(commoner);
        }

        [Test]
        public void StressFirstLevelCommoner()
        {
            Stress(AssertFirstLevelCommoner);
        }

        private void AssertFirstLevelCommoner()
        {
            SetClassNameRandomizer.SetClassName = CharacterClassConstants.Commoner;
            SetLevelRandomizer.SetLevel = 1;

            var commoner = CharacterGenerator.GenerateWith(AlignmentRandomizer, SetClassNameRandomizer, SetLevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(commoner);
        }

        [Test]
        public void StressSpellcaster()
        {
            Stress(AssertSpellcaster);
        }

        private void AssertSpellcaster()
        {
            var spellcaster = Generate(() => CharacterGenerator.GenerateWith(AlignmentRandomizer, SpellcasterClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, HeroicStatsRandomizer),
                c => c.Class.Level > 3);

            CharacterVerifier.AssertCharacter(spellcaster);
            Assert.That(spellcaster.Equipment.Treasure.Items, Is.Not.Empty);

            Assert.That(spellcaster.Magic, Is.Not.Null);
            Assert.That(spellcaster.Magic.Animal, Is.Not.Null);
            Assert.That(spellcaster.Magic.ArcaneSpellFailure, Is.InRange(0, 100));

            Assert.That(spellcaster.Magic.KnownSpells, Is.Not.Empty, spellcaster.Class.Name);

            foreach (var knownSpell in spellcaster.Magic.KnownSpells)
            {
                Assert.That(knownSpell.Level, Is.InRange(0, 9));
                Assert.That(knownSpell.Metamagic, Is.Empty);
                Assert.That(knownSpell.Name, Is.Not.Empty);
            }

            var knownSpellLevels = spellcaster.Magic.KnownSpells.Select(s => s.Level).Distinct();
            var maxSpellLevel = knownSpellLevels.Max();
            var minSpellLevel = knownSpellLevels.Min();
            Assert.That(minSpellLevel, Is.EqualTo(0).Or.EqualTo(1), $"{spellcaster.Class.Name} minimum known spell level");
            Assert.That(knownSpellLevels.Count(), Is.EqualTo(maxSpellLevel - minSpellLevel + 1), $"{spellcaster.Class.Name} known spell levels count");

            Assert.That(spellcaster.Magic.SpellsPerDay, Is.Not.Empty, spellcaster.Class.Name);

            foreach (var spellQuantity in spellcaster.Magic.SpellsPerDay)
            {
                Assert.That(spellQuantity.Level, Is.InRange(minSpellLevel, maxSpellLevel));
                Assert.That(spellQuantity.Quantity, Is.Not.Negative);

                if (spellQuantity.HasDomainSpell == false)
                    Assert.That(spellQuantity.Quantity, Is.Positive);

                if (spellQuantity.Level > 0)
                    Assert.That(spellQuantity.HasDomainSpell, Is.EqualTo(spellcaster.Class.SpecialistFields.Any()));
                else
                    Assert.That(spellQuantity.HasDomainSpell, Is.False);
            }

            var spellsPerDayLevels = spellcaster.Magic.SpellsPerDay.Select(s => s.Level);
            Assert.That(spellsPerDayLevels.Min(), Is.EqualTo(minSpellLevel), $"{spellcaster.Class.Name} min per day spell level");
            Assert.That(spellsPerDayLevels.Max(), Is.InRange(maxSpellLevel - 1, maxSpellLevel), $"{spellcaster.Class.Name} max per day spell level");
            Assert.That(spellsPerDayLevels, Is.Unique);
            Assert.That(spellsPerDayLevels.Count(), Is.InRange(maxSpellLevel - minSpellLevel, maxSpellLevel - minSpellLevel + 1), $"{spellcaster.Class.Name} spells per day count");

            if (spellcaster.Class.Name == CharacterClassConstants.Bard || spellcaster.Class.Name == CharacterClassConstants.Sorcerer)
            {
                Assert.That(spellcaster.Magic.PreparedSpells, Is.Empty, spellcaster.Class.Name);
            }
            else
            {
                Assert.That(spellcaster.Magic.PreparedSpells, Is.Not.Empty, spellcaster.Class.Name);

                foreach (var preparedSpell in spellcaster.Magic.PreparedSpells)
                {
                    Assert.That(preparedSpell.Level, Is.InRange(minSpellLevel, maxSpellLevel));
                    Assert.That(preparedSpell.Metamagic, Is.Empty);
                    Assert.That(preparedSpell.Name, Is.Not.Empty);
                }

                var preparedSpellLevels = spellcaster.Magic.PreparedSpells.Select(s => s.Level).Distinct();
                Assert.That(preparedSpellLevels.Min(), Is.EqualTo(minSpellLevel), $"{spellcaster.Class.Name} min prepared spell level");
                Assert.That(preparedSpellLevels.Max(), Is.InRange(maxSpellLevel - 1, maxSpellLevel), $"{spellcaster.Class.Name} max prepared spell level");
                Assert.That(preparedSpellLevels.Count(), Is.InRange(maxSpellLevel - minSpellLevel, maxSpellLevel - minSpellLevel + 1), $"{spellcaster.Class.Name} prepared spell level count");
            }
        }

        [Test]
        public void StressSetMetarace()
        {
            Stress(AssertSetMetarace);
        }

        private void AssertSetMetarace()
        {
            var forcableMetaraceRandomizer = MetaraceRandomizer as IForcableMetaraceRandomizer;
            forcableMetaraceRandomizer.ForceMetarace = true;

            var setMetaraceRandomizer = GetNewInstanceOf<ISetMetaraceRandomizer>();

            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            setMetaraceRandomizer.SetMetarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, setMetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(character);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty);
        }

        [Test]
        public void StressSetMetaraceAndSetLevelAllowingValidAdjustment()
        {
            Stress(AssertSetMetaraceAndSetLevelAllowingValidAdjustment);
        }

        private void AssertSetMetaraceAndSetLevelAllowingValidAdjustment()
        {
            var forcableMetaraceRandomizer = MetaraceRandomizer as IForcableMetaraceRandomizer;
            forcableMetaraceRandomizer.ForceMetarace = true;

            var setMetaraceRandomizer = GetNewInstanceOf<ISetMetaraceRandomizer>();

            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            setMetaraceRandomizer.SetMetarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);

            var setLevelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            setLevelRandomizer.SetLevel = Math.Max(LevelRandomizer.Randomize(), 9);
            setLevelRandomizer.AllowAdjustments = true;

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, setLevelRandomizer, BaseRaceRandomizer, setMetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(character);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty);
        }

        [Test]
        public void StressSetMetaraceAndSetLevelAllowingInvalidAdjustment()
        {
            Stress(AssertSetMetaraceAndSetLevelAllowingInvalidAdjustment);
        }

        private void AssertSetMetaraceAndSetLevelAllowingInvalidAdjustment()
        {
            var forcableMetaraceRandomizer = MetaraceRandomizer as IForcableMetaraceRandomizer;
            forcableMetaraceRandomizer.ForceMetarace = true;

            var setMetaraceRandomizer = GetNewInstanceOf<ISetMetaraceRandomizer>();

            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            setMetaraceRandomizer.SetMetarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);

            var setLevelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            setLevelRandomizer.SetLevel = 1;
            setLevelRandomizer.AllowAdjustments = true;

            Assert.That(() => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, setLevelRandomizer, BaseRaceRandomizer, setMetaraceRandomizer, RawStatsRandomizer), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void StressSetMetaraceAndSetLevelNotAllowingAdjustment()
        {
            Stress(AssertSetMetaraceAndSetLevelNotAllowingAdjustment);
        }

        private void AssertSetMetaraceAndSetLevelNotAllowingAdjustment()
        {
            var forcableMetaraceRandomizer = MetaraceRandomizer as IForcableMetaraceRandomizer;
            forcableMetaraceRandomizer.ForceMetarace = true;

            var setMetaraceRandomizer = GetNewInstanceOf<ISetMetaraceRandomizer>();

            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            setMetaraceRandomizer.SetMetarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);

            var setLevelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            setLevelRandomizer.SetLevel = LevelRandomizer.Randomize();
            setLevelRandomizer.AllowAdjustments = false;

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, setLevelRandomizer, BaseRaceRandomizer, setMetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(character);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty);
        }

        [Test]
        public void StressUndead()
        {
            Stress(AssertUndead);
        }

        private void AssertUndead()
        {
            var undeadMetaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta);
            undeadMetaraceRandomizer.ForceMetarace = true;

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, undeadMetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(character);
            Assert.That(character.Race.Metarace, Is.EqualTo(RaceConstants.Metaraces.Ghost)
                .Or.EqualTo(RaceConstants.Metaraces.Lich)
                .Or.EqualTo(RaceConstants.Metaraces.Vampire));
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty, character.Race.Metarace);
            Assert.That(character.Ability.Stats.Keys, Is.All.Not.EqualTo(StatConstants.Constitution), character.Race.Metarace);
            Assert.That(character.Combat.SavingThrows.HasFortitudeSave, Is.False, character.Race.Metarace);
        }
    }
}