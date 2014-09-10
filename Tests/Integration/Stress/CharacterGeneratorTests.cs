using System;
using System.Collections.Generic;
using System.Linq;
using EquipmentGen.Common.Items;
using Ninject;
using NPCGen.Common;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Common.Races;
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
            Assert.That(character.InterestingTrait, Is.Not.Null);
            Assert.That(baseRaces, Contains.Item(character.Race.BaseRace));
            Assert.That(metaraces, Contains.Item(character.Race.Metarace));

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

            Assert.That(character.Equipment.Armor.ItemType, Is.EqualTo(ItemTypeConstants.Armor));
            Assert.That(character.Equipment.Armor.Name, Is.Not.Empty);
            Assert.That(character.Equipment.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(character.Equipment.PrimaryHand.Name, Is.Not.Empty);
            Assert.That(character.Equipment.Treasure, Is.Not.Null);
            Assert.That(character.Equipment.OffHand, Is.Not.Null);

            if (character.Equipment.OffHand.ItemType == ItemTypeConstants.Armor)
                Assert.That(character.Equipment.OffHand.Attributes, Contains.Item(AttributeConstants.Shield));
            else if (character.Equipment.OffHand.ItemType == ItemTypeConstants.Weapon && character.Equipment.OffHand != character.Equipment.PrimaryHand)
                Assert.That(character.Equipment.OffHand.Attributes, Is.Not.Contains(WeaponAttributeConstants.TwoHanded));
            else if (character.Equipment.OffHand != character.Equipment.PrimaryHand)
                Assert.That(character.Equipment.OffHand.Name, Is.Empty);

            Assert.That(character.Magic.Familiar, Is.Not.Null);

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
        }

        [Test]
        public void FamiliarsHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && String.IsNullOrEmpty(character.Magic.Familiar.Animal));

            AssertIterations();
            Assert.That(character.Magic.Familiar.Animal, Is.Not.Empty);
        }

        [Test]
        public void FamiliarsDoNotHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && !String.IsNullOrEmpty(character.Magic.Familiar.Animal));

            AssertIterations();
            Assert.That(character.Magic.Familiar.Animal, Is.Empty);
        }

        [Test]
        public void InterestingTraitsHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && String.IsNullOrEmpty(character.InterestingTrait));

            AssertIterations();
            Assert.That(character.InterestingTrait, Is.Not.Empty);
        }

        [Test]
        public void InterestingTraitsDoNotHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && !String.IsNullOrEmpty(character.InterestingTrait));

            AssertIterations();
            Assert.That(character.InterestingTrait, Is.Empty);
        }

        [Test]
        public void SpellsHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && !character.Magic.Spells.Any());

            AssertIterations();
            Assert.That(character.Magic.Spells, Is.Not.Empty);
        }

        [Test]
        public void SpellsDoNotHappen()
        {
            var character = new Character();

            do character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
            while (TestShouldKeepRunning() && character.Magic.Spells.Any());

            AssertIterations();
            Assert.That(character.Magic.Spells, Is.Empty);
        }
    }
}