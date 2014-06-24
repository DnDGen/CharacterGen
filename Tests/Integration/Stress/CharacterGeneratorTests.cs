using System;
using System.Collections.Generic;
using System.Linq;
using EquipmentGen.Common.Items;
using Ninject;
using NPCGen.Common;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class CharacterGeneratorTests : StressTests
    {
        [Inject]
        public ICharacterGenerator CharacterGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }

        IEnumerable<String> goodnesses;
        IEnumerable<String> lawfulnesses;
        IEnumerable<String> classNames;
        IEnumerable<String> baseRaces;
        IEnumerable<String> metaraces;

        [SetUp]
        public void Setup()
        {
            goodnesses = AlignmentConstants.GetGoodnesses();
            lawfulnesses = AlignmentConstants.GetLawfulnesses();
            classNames = CharacterClassConstants.GetClassNames();
            baseRaces = RaceConstants.BaseRaces.GetBaseRaces();
            metaraces = RaceConstants.Metaraces.GetMetaraces().Union(new[] { String.Empty });
        }

        protected override void MakeAssertions()
        {
            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                    MetaraceRandomizer, StatsRandomizer);

            Assert.That(goodnesses, Contains.Item(character.Alignment.Goodness));
            Assert.That(lawfulnesses, Contains.Item(character.Alignment.Lawfulness));
            Assert.That(classNames, Contains.Item(character.Class.ClassName));
            Assert.That(character.Class.Level, Is.Positive);
            Assert.That(character.Class.BaseAttack.Bonus, Is.Not.Negative);
            Assert.That(character.HitPoints, Is.AtLeast(character.Class.Level));
            Assert.That(character.InterestingTrait, Is.Not.Null);
            Assert.That(character.Languages.Count(), Is.AtLeast(1));
            Assert.That(baseRaces, Contains.Item(character.Race.BaseRace));
            Assert.That(metaraces, Contains.Item(character.Race.Metarace));

            Assert.That(character.Stats.Count, Is.EqualTo(6));
            Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Charisma));
            Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Constitution));
            Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Dexterity));
            Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Intelligence));
            Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Strength));
            Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Wisdom));
            Assert.That(character.Stats[StatConstants.Charisma].Value, Is.Positive);
            Assert.That(character.Stats[StatConstants.Constitution].Value, Is.Positive);
            Assert.That(character.Stats[StatConstants.Dexterity].Value, Is.Positive);
            Assert.That(character.Stats[StatConstants.Intelligence].Value, Is.Positive);
            Assert.That(character.Stats[StatConstants.Strength].Value, Is.Positive);
            Assert.That(character.Stats[StatConstants.Wisdom].Value, Is.Positive);

            Assert.That(character.Skills, Is.Not.Empty);
            Assert.That(character.Feats, Is.Not.Empty);
            Assert.That(character.SavingThrows.Reflex, Is.Not.Negative);
            Assert.That(character.SavingThrows.Fortitude, Is.Not.Negative);
            Assert.That(character.SavingThrows.Will, Is.Not.Negative);

            Assert.That(character.Armor.ItemType, Is.EqualTo(ItemTypeConstants.Armor));
            Assert.That(character.Armor.Name, Is.Not.Empty);
            Assert.That(character.ArmorClass.Full, Is.Positive);
            Assert.That(character.ArmorClass.FlatFooted, Is.Positive);
            Assert.That(character.ArmorClass.Touch, Is.Positive);
            Assert.That(character.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(character.PrimaryHand.Name, Is.Not.Empty);
            Assert.That(character.Familiar, Is.Not.Null);
            Assert.That(character.Treasure, Is.Not.Null);
            Assert.That(character.OffHand, Is.Not.Null);

            if (!String.IsNullOrEmpty(character.OffHand.Name))
            {
                Assert.That(character.OffHand.ItemType, Is.EqualTo(ItemTypeConstants.Armor).Or.EqualTo(ItemTypeConstants.Weapon));

                if (character.OffHand.ItemType == ItemTypeConstants.Armor)
                    Assert.That(character.OffHand.Attributes, Contains.Item(AttributeConstants.Shield));
            }

            foreach (var level in character.Spells.Keys)
                Assert.That(character.Spells[level], Is.Not.Empty, level.ToString());
        }

        [Test]
        public void FamiliarsHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && String.IsNullOrEmpty(character.Familiar.Animal));

            Assert.That(character.Familiar.Animal, Is.Not.Empty);
            AssertIterations();
        }

        [Test]
        public void FamiliarsDoNotHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && !String.IsNullOrEmpty(character.Familiar.Animal));

            Assert.That(character.Familiar.Animal, Is.Empty);
            AssertIterations();
        }

        [Test]
        public void InterestingTraitsHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && String.IsNullOrEmpty(character.InterestingTrait));

            Assert.That(character.InterestingTrait, Is.Not.Empty);
            AssertIterations();
        }

        [Test]
        public void InterestingTraitsDoNotHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && !String.IsNullOrEmpty(character.InterestingTrait));

            Assert.That(character.InterestingTrait, Is.Empty);
            AssertIterations();
        }

        [Test]
        public void OffHandHappens()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && (String.IsNullOrEmpty(character.OffHand.Name) || character.OffHand == character.PrimaryHand));

            Assert.That(character.OffHand.Name, Is.Not.Empty);
            Assert.That(character.OffHand, Is.Not.EqualTo(character.PrimaryHand));
            AssertIterations();
        }

        [Test]
        public void TwoHandedHappens()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && character.OffHand != character.PrimaryHand);

            Assert.That(character.OffHand, Is.EqualTo(character.PrimaryHand));
            AssertIterations();
        }

        [Test]
        public void OffHandDoesNotHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && !String.IsNullOrEmpty(character.OffHand.Name));

            Assert.That(character.OffHand.Name, Is.Empty);
            AssertIterations();
        }

        [Test]
        public void SpellsHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && !character.Spells.Any());

            Assert.That(character.Spells, Is.Not.Empty);
            AssertIterations();
        }

        [Test]
        public void SpellsDoNotHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && character.Spells.Any());

            Assert.That(character.Spells, Is.Empty);
            AssertIterations();
        }

        [Test]
        public void CoinHappens()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && character.Treasure.Coin.Quantity == 0);

            Assert.That(character.Treasure.Coin.Quantity, Is.Positive);
            AssertIterations();
        }

        [Test]
        public void GoodsHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && !character.Treasure.Goods.Any());

            Assert.That(character.Treasure.Goods, Is.Not.Empty);
            AssertIterations();
        }

        [Test]
        public void ItemsHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && !character.Treasure.Items.Any());

            Assert.That(character.Treasure.Items, Is.Not.Empty);
            AssertIterations();
        }

        [Test]
        public void TreasureDoesNotHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && (character.Treasure.Items.Any() || character.Treasure.Goods.Any()
                || character.Treasure.Coin.Quantity > 0));

            Assert.That(character.Treasure.Items, Is.Empty);
            Assert.That(character.Treasure.Goods, Is.Empty);
            Assert.That(character.Treasure.Coin.Quantity, Is.EqualTo(0));
            AssertIterations();
        }
    }
}