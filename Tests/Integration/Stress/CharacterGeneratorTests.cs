﻿using CharacterGen.Common;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.Races;
using CharacterGen.Generators;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Generators.Verifiers.Exceptions;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress
{
    [TestFixture]
    public class CharacterGeneratorTests : StressTests
    {
        [Inject]
        public ICharacterGenerator CharacterGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyNPC)]
        public IClassNameRandomizer NPCClassNameRandomizer { get; set; }

        [TestCase("Character Generator")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer);

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
            var npc = CharacterGenerator.GenerateWith(AlignmentRandomizer, NPCClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer);

            AssertCharacter(npc);
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

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, setMetaraceRandomizer, StatsRandomizer);

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

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, setLevelRandomizer, BaseRaceRandomizer, setMetaraceRandomizer, StatsRandomizer);

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

            Assert.That(() => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, setLevelRandomizer, BaseRaceRandomizer, setMetaraceRandomizer, StatsRandomizer), Throws.InstanceOf<IncompatibleRandomizersException>());
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

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, setLevelRandomizer, BaseRaceRandomizer, setMetaraceRandomizer, StatsRandomizer);

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

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, setLevelRandomizer, BaseRaceRandomizer, setMetaraceRandomizer, StatsRandomizer);

            AssertCharacter(character);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty, character.Class.ClassName);
        }
    }
}