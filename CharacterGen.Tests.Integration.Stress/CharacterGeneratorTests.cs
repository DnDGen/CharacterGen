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

            Assert.That(character.Class.ClassName, Is.Not.Empty);
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
            Assert.That(spellcaster.Magic.SpellsPerDay, Is.Not.Empty);
            Assert.That(spellcaster.Magic.ArcaneSpellFailure, Is.InRange(0, 100));

            foreach (var spell in spellcaster.Magic.SpellsPerDay)
            {
                Assert.That(spell.Level, Is.InRange(0, 9));
                Assert.That(spell.Quantity, Is.Positive);

                if (spell.Level > 0)
                    Assert.That(spell.HasDomainSpell, Is.EqualTo(spellcaster.Class.SpecialistFields.Any()));
                else
                    Assert.That(spell.HasDomainSpell, Is.False);
            }

            var spellLevels = spellcaster.Magic.SpellsPerDay.Select(s => s.Level);
            Assert.That(spellLevels, Is.Unique);

            var maxSpellLevel = spellLevels.Max();
            var minSpellLevel = spellLevels.Min();
            Assert.That(minSpellLevel, Is.EqualTo(0).Or.EqualTo(1));
            Assert.That(spellLevels.Count(), Is.EqualTo(maxSpellLevel - minSpellLevel + 1));
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

        [TestCase(RaceConstants.Metaraces.Ghost, 1)]
        [TestCase(RaceConstants.Metaraces.Ghost, 2)]
        [TestCase(RaceConstants.Metaraces.Ghost, 3)]
        [TestCase(RaceConstants.Metaraces.Ghost, 4)]
        [TestCase(RaceConstants.Metaraces.Ghost, 5)]
        [TestCase(RaceConstants.Metaraces.Ghost, 6)]
        [TestCase(RaceConstants.Metaraces.Ghost, 7)]
        [TestCase(RaceConstants.Metaraces.Ghost, 8)]
        [TestCase(RaceConstants.Metaraces.Ghost, 9)]
        [TestCase(RaceConstants.Metaraces.Ghost, 10)]
        [TestCase(RaceConstants.Metaraces.Ghost, 11)]
        [TestCase(RaceConstants.Metaraces.Ghost, 12)]
        [TestCase(RaceConstants.Metaraces.Ghost, 13)]
        [TestCase(RaceConstants.Metaraces.Ghost, 14)]
        [TestCase(RaceConstants.Metaraces.Ghost, 15)]
        [TestCase(RaceConstants.Metaraces.Ghost, 16)]
        [TestCase(RaceConstants.Metaraces.Ghost, 17)]
        [TestCase(RaceConstants.Metaraces.Ghost, 18)]
        [TestCase(RaceConstants.Metaraces.Ghost, 19)]
        [TestCase(RaceConstants.Metaraces.Ghost, 20)]
        [TestCase(RaceConstants.Metaraces.Lich, 1)]
        [TestCase(RaceConstants.Metaraces.Lich, 2)]
        [TestCase(RaceConstants.Metaraces.Lich, 3)]
        [TestCase(RaceConstants.Metaraces.Lich, 4)]
        [TestCase(RaceConstants.Metaraces.Lich, 5)]
        [TestCase(RaceConstants.Metaraces.Lich, 6)]
        [TestCase(RaceConstants.Metaraces.Lich, 7)]
        [TestCase(RaceConstants.Metaraces.Lich, 8)]
        [TestCase(RaceConstants.Metaraces.Lich, 9)]
        [TestCase(RaceConstants.Metaraces.Lich, 10)]
        [TestCase(RaceConstants.Metaraces.Lich, 11)]
        [TestCase(RaceConstants.Metaraces.Lich, 12)]
        [TestCase(RaceConstants.Metaraces.Lich, 13)]
        [TestCase(RaceConstants.Metaraces.Lich, 14)]
        [TestCase(RaceConstants.Metaraces.Lich, 15)]
        [TestCase(RaceConstants.Metaraces.Lich, 16)]
        [TestCase(RaceConstants.Metaraces.Lich, 17)]
        [TestCase(RaceConstants.Metaraces.Lich, 18)]
        [TestCase(RaceConstants.Metaraces.Lich, 19)]
        [TestCase(RaceConstants.Metaraces.Lich, 20)]
        [TestCase(RaceConstants.Metaraces.Vampire, 1)]
        [TestCase(RaceConstants.Metaraces.Vampire, 2)]
        [TestCase(RaceConstants.Metaraces.Vampire, 3)]
        [TestCase(RaceConstants.Metaraces.Vampire, 4)]
        [TestCase(RaceConstants.Metaraces.Vampire, 5)]
        [TestCase(RaceConstants.Metaraces.Vampire, 6)]
        [TestCase(RaceConstants.Metaraces.Vampire, 7)]
        [TestCase(RaceConstants.Metaraces.Vampire, 8)]
        [TestCase(RaceConstants.Metaraces.Vampire, 9)]
        [TestCase(RaceConstants.Metaraces.Vampire, 10)]
        [TestCase(RaceConstants.Metaraces.Vampire, 11)]
        [TestCase(RaceConstants.Metaraces.Vampire, 12)]
        [TestCase(RaceConstants.Metaraces.Vampire, 13)]
        [TestCase(RaceConstants.Metaraces.Vampire, 14)]
        [TestCase(RaceConstants.Metaraces.Vampire, 15)]
        [TestCase(RaceConstants.Metaraces.Vampire, 16)]
        [TestCase(RaceConstants.Metaraces.Vampire, 17)]
        [TestCase(RaceConstants.Metaraces.Vampire, 18)]
        [TestCase(RaceConstants.Metaraces.Vampire, 19)]
        [TestCase(RaceConstants.Metaraces.Vampire, 20)]
        public void StressUndead(string undead, int level)
        {
            Stress(() => AssertUndead(undead, level));
        }

        private void AssertUndead(string undead, int level)
        {
            var setMetaraceRandomizer = GetNewInstanceOf<ISetMetaraceRandomizer>();
            setMetaraceRandomizer.SetMetarace = undead;

            var setLevelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            setLevelRandomizer.SetLevel = level;
            setLevelRandomizer.AllowAdjustments = false;

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, setLevelRandomizer, BaseRaceRandomizer, setMetaraceRandomizer, RawStatsRandomizer);

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