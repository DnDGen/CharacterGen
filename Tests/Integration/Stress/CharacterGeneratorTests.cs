using CharacterGen.Common;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Stress
{
    [TestFixture]
    public class CharacterGeneratorTests : StressTests
    {
        [Inject]
        public ICharacterGenerator CharacterGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public ISetLevelRandomizer SetLevelRandomizer { get; set; }

        IEnumerable<String> classNames;

        [SetUp]
        public void Setup()
        {
            classNames = new[] {
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard
            };
        }

        [TestCase("CharacterGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                    MetaraceRandomizer, StatsRandomizer);

            Assert.That(character.Alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Evil));
            Assert.That(character.Alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Chaotic));

            Assert.That(classNames, Contains.Item(character.Class.ClassName));
            Assert.That(character.Class.Level, Is.Positive);
            Assert.That(character.InterestingTrait, Is.Not.Null);
            Assert.That(character.Race.BaseRace, Is.Not.Empty);
            Assert.That(character.Race.Metarace, Is.Not.Empty);

            Assert.That(character.Ability.Stats.Count, Is.EqualTo(6));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Charisma));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Constitution));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Dexterity));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Intelligence));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Strength));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Wisdom));
            Assert.That(character.Ability.Stats[StatConstants.Charisma].Value, Is.Positive);
            Assert.That(character.Ability.Stats[StatConstants.Constitution].Value, Is.Positive);
            Assert.That(character.Ability.Stats[StatConstants.Dexterity].Value, Is.Positive);
            Assert.That(character.Ability.Stats[StatConstants.Intelligence].Value, Is.Positive);
            Assert.That(character.Ability.Stats[StatConstants.Strength].Value, Is.Positive);
            Assert.That(character.Ability.Stats[StatConstants.Wisdom].Value, Is.Positive);
            Assert.That(character.Ability.Languages, Is.Not.Empty);
            Assert.That(character.Ability.Skills, Is.Not.Empty);
            Assert.That(character.Ability.Feats, Is.Not.Empty);

            Assert.That(character.Equipment.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(character.Equipment.PrimaryHand.Name, Is.Not.Empty);
            Assert.That(character.Equipment.Treasure, Is.Not.Null);

            foreach (var level in character.Magic.Spells.Keys)
                Assert.That(character.Magic.Spells[level], Is.Not.Empty, level.ToString());

            Assert.That(character.Combat.BaseAttack.Bonus, Is.Not.Negative);
            Assert.That(character.Combat.HitPoints, Is.AtLeast(character.Class.Level));
            Assert.That(character.Combat.SavingThrows.Reflex, Is.Not.Negative);
            Assert.That(character.Combat.SavingThrows.Fortitude, Is.Not.Negative);
            Assert.That(character.Combat.SavingThrows.Will, Is.Not.Negative);
            Assert.That(character.Combat.ArmorClass.Full, Is.Positive);
            Assert.That(character.Combat.ArmorClass.FlatFooted, Is.Positive);
            Assert.That(character.Combat.ArmorClass.Touch, Is.Positive);

            Assert.That(character.Leadership, Is.Not.Null);
        }

        [Test]
        public void FamiliarsHappen()
        {
            var character = Generate<Character>(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => String.IsNullOrEmpty(c.Magic.Familiar.Animal) == false);

            Assert.That(character.Magic.Familiar.Animal, Is.Not.Empty);
        }

        [Test]
        public void FamiliarsDoNotHappen()
        {
            var character = Generate<Character>(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => String.IsNullOrEmpty(c.Magic.Familiar.Animal));

            Assert.That(character.Magic.Familiar.Animal, Is.Empty);
        }

        [Test]
        public void InterestingTraitsHappen()
        {
            var character = Generate<Character>(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => String.IsNullOrEmpty(c.InterestingTrait) == false);

            Assert.That(character.InterestingTrait, Is.Not.Empty);
        }

        [Test]
        public void InterestingTraitsDoNotHappen()
        {
            var character = Generate<Character>(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => String.IsNullOrEmpty(c.InterestingTrait));

            Assert.That(character.InterestingTrait, Is.Empty);
        }

        [Test]
        public void SpellsHappen()
        {
            var character = Generate<Character>(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => c.Magic.Spells.Any());

            Assert.That(character.Magic.Spells, Is.Not.Empty);
        }

        [Test]
        public void SpellsDoNotHappen()
        {
            var character = Generate<Character>(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => c.Magic.Spells.Any() == false);

            Assert.That(character.Magic.Spells, Is.Empty);
        }

        [Test]
        public void LeadershipHappens()
        {
            SetLevelRandomizer.SetLevel = 20;

            var character = Generate<Character>(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => c.Leadership.Score > 0);

            Assert.That(character.Leadership.Score, Is.Positive);
            Assert.That(character.Leadership.Cohort, Is.Not.Null);
            Assert.That(character.Leadership.Followers, Is.Not.Empty);
        }

        [Test]
        public void LeadershipDoesNotHappen()
        {
            SetLevelRandomizer.SetLevel = 20;

            var character = Generate<Character>(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => c.Leadership.Score == 0);

            Assert.That(character.Leadership.Score, Is.EqualTo(0));
            Assert.That(character.Leadership.Cohort, Is.Null);
            Assert.That(character.Leadership.Followers, Is.Empty);
        }
    }
}