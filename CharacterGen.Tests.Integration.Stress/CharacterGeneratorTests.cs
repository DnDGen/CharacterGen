using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Randomizers.Stats;
using CharacterGen.Verifiers.Exceptions;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

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

        [TestCase("Character Generator")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawStatsRandomizer);

            AssertCharacter(character);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty);
        }

        private void AssertCharacter(Character character)
        {
            Assert.That(character.Alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Evil));
            Assert.That(character.Alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Chaotic));

            Assert.That(character.Class.Name, Is.Not.Empty);
            Assert.That(character.Class.Level, Is.Positive);
            Assert.That(character.Class.ProhibitedFields, Is.Not.Null);
            Assert.That(character.Class.SpecialistFields, Is.Not.Null);

            Assert.That(character.InterestingTrait, Is.Not.Null);

            Assert.That(character.Race.BaseRace, Is.Not.Empty);
            Assert.That(character.Race.Metarace, Is.Not.Null);
            Assert.That(character.Race.AerialSpeed, Is.Not.Negative);
            Assert.That(character.Race.AerialSpeed % 10, Is.EqualTo(0));
            Assert.That(character.Race.Age.Stage, Is.Not.Empty);
            Assert.That(character.Race.Age.Years, Is.Positive);
            Assert.That(character.Race.HeightInInches, Is.Positive);
            Assert.That(character.Race.WeightInPounds, Is.Positive);

            if (character.Race.HasWings)
                Assert.That(character.Race.AerialSpeed, Is.Positive);

            Assert.That(character.Race.LandSpeed, Is.Positive);
            Assert.That(character.Race.LandSpeed % 10, Is.EqualTo(0));
            Assert.That(character.Race.MetaraceSpecies, Is.Not.Null);
            Assert.That(character.Race.Size, Is.EqualTo(RaceConstants.Sizes.Large)
                .Or.EqualTo(RaceConstants.Sizes.Medium)
                .Or.EqualTo(RaceConstants.Sizes.Small));

            Assert.That(character.Ability.Stats.Count, Is.EqualTo(6));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Charisma));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Constitution));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Dexterity));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Intelligence));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Strength));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Wisdom));
            Assert.That(character.Ability.Stats[StatConstants.Charisma].Value, Is.Positive);
            Assert.That(character.Ability.Stats[StatConstants.Constitution].Value, Is.Not.Negative);
            Assert.That(character.Ability.Stats[StatConstants.Dexterity].Value, Is.Positive);
            Assert.That(character.Ability.Stats[StatConstants.Intelligence].Value, Is.Positive);
            Assert.That(character.Ability.Stats[StatConstants.Strength].Value, Is.Positive);
            Assert.That(character.Ability.Stats[StatConstants.Wisdom].Value, Is.Positive);
            Assert.That(character.Ability.Languages, Is.Not.Empty);
            Assert.That(character.Ability.Skills, Is.Not.Empty);

            foreach (var skill in character.Ability.Skills)
                Assert.That(skill.Value.ArmorCheckPenalty, Is.AtMost(0));

            Assert.That(character.Ability.Feats, Is.Not.Empty);

            foreach (var feat in character.Ability.Feats)
            {
                Assert.That(feat.Name, Is.Not.Empty);
                Assert.That(feat.Foci, Is.Not.Null, feat.Name);
                Assert.That(feat.Power, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.Quantity, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Day)
                    .Or.EqualTo(FeatConstants.Frequencies.Week)
                    .Or.EqualTo(FeatConstants.Frequencies.Round)
                    .Or.EqualTo(FeatConstants.Frequencies.Hit)
                    .Or.Empty, feat.Name);

                if (feat.Name == FeatConstants.SaveBonus)
                    Assert.That(feat.Foci, Is.Not.Empty, character.Race.BaseRace);
            }


            if (character.Ability.Feats.SelectMany(f => f.Foci).Any(f => f == FeatConstants.Foci.UnarmedStrike) == false)
            {
                var feats = GetAllFeatsMessage(character.Ability.Feats);

                Assert.That(character.Equipment.PrimaryHand, Is.Not.Null, feats);
                Assert.That(character.Equipment.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), feats);
                Assert.That(character.Equipment.PrimaryHand.Name, Is.Not.Empty, feats);
            }

            Assert.That(character.Equipment.Treasure, Is.Not.Null);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Null);

            foreach (var item in character.Equipment.Treasure.Items)
                Assert.That(item, Is.Not.Null);

            foreach (var spells in character.Magic.SpellsPerDay)
            {
                Assert.That(spells.Level, Is.Not.Negative, spells.Level.ToString());
                Assert.That(spells.Quantity, Is.Not.Negative, spells.Level.ToString());
            }

            foreach (var spell in character.Magic.KnownSpells)
            {
                Assert.That(spell.Level, Is.Not.Negative);
                Assert.That(spell.Metamagic, Is.Empty);
                Assert.That(spell.Name, Is.Not.Empty);
            }

            foreach (var spell in character.Magic.PreparedSpells)
            {
                Assert.That(spell.Level, Is.Not.Negative);
                Assert.That(spell.Metamagic, Is.Empty);
                Assert.That(spell.Name, Is.Not.Empty);

                var knownSpellNames = character.Magic.KnownSpells.Select(s => s.Name);
                Assert.That(knownSpellNames, Contains.Item(spell.Name), character.Class.Name);
            }

            Assert.That(character.Combat.BaseAttack.Bonus, Is.Not.Negative);
            Assert.That(character.Combat.HitPoints, Is.AtLeast(character.Class.Level));
            Assert.That(character.Combat.ArmorClass.Full, Is.Positive);
            Assert.That(character.Combat.ArmorClass.FlatFooted, Is.Positive);
            Assert.That(character.Combat.ArmorClass.Touch, Is.Positive);
        }

        [Test]
        public void StressNPC()
        {
            Stress(AssertNPC);
        }

        private void AssertNPC()
        {
            var npc = CharacterGenerator.GenerateWith(AlignmentRandomizer, NPCClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawStatsRandomizer);

            AssertCharacter(npc);
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

            AssertCharacter(commoner);
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

            AssertCharacter(commoner);
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

            AssertCharacter(spellcaster);
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

            AssertCharacter(character);
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

            AssertCharacter(character);
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

            AssertCharacter(character);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty);
        }

        [Test]
        public void StressUndead()
        {
            Stress(AssertUndead);
        }

        private void AssertUndead()
        {
            var undeadMetaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta);

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, undeadMetaraceRandomizer, RawStatsRandomizer);

            AssertCharacter(character);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty);
        }

        private string GetAllFeatsMessage(IEnumerable<Feat> feats)
        {
            var featsWithFoci = feats.Where(f => f.Foci.Any()).Select(f => $"{f.Name}: {string.Join(", ", f.Foci)}").OrderBy(f => f);
            return string.Join("; ", featsWithFoci);
        }
    }
}