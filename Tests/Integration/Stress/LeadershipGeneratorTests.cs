﻿using CharacterGen.Common;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.Races;
using CharacterGen.Generators;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Ninject;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Stress
{
    [TestFixture]
    public class LeadershipGeneratorTests : StressTests
    {
        [Inject]
        public ILeadershipGenerator LeadershipGenerator { get; set; }
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject]
        public IAnimalGenerator AnimalGenerator { get; set; }
        [Inject]
        public Random Random { get; set; }
        [Inject]
        public ICollectionsSelector CollectionsSelector { get; set; }

        [TestCase("Leadership Generator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = Generate(() => GetNewCharacterClass(alignment), c => c.Level > 5);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
            var animal = AnimalGenerator.GenerateFrom(alignment, characterClass, race, ability.Feats);

            var leadership = LeadershipGenerator.GenerateLeadership(characterClass.Level, ability.Stats[StatConstants.Charisma].Bonus, animal);
            Assert.That(leadership, Is.Not.Null);
            Assert.That(leadership.CohortScore, Is.Not.Negative);
            Assert.That(leadership.Score, Is.Positive);
            Assert.That(leadership.FollowerQuantities.Level1, Is.Not.Negative);
            Assert.That(leadership.FollowerQuantities.Level2, Is.InRange(0, leadership.FollowerQuantities.Level1));
            Assert.That(leadership.FollowerQuantities.Level3, Is.InRange(0, leadership.FollowerQuantities.Level2));
            Assert.That(leadership.FollowerQuantities.Level4, Is.InRange(0, leadership.FollowerQuantities.Level3));
            Assert.That(leadership.FollowerQuantities.Level5, Is.InRange(0, leadership.FollowerQuantities.Level4));
            Assert.That(leadership.FollowerQuantities.Level6, Is.InRange(0, leadership.FollowerQuantities.Level5));
            Assert.That(leadership.LeadershipModifiers, Is.Not.Null);
        }

        [Test]
        public void StressCohort()
        {
            Stress(AssertCohort);
        }

        public void AssertCohort()
        {
            var leaderAlignment = GetNewAlignment();
            var leaderLevel = Random.Next(6, 21);
            var cohortScore = Random.Next(3, 26);

            var cohort = LeadershipGenerator.GenerateCohort(cohortScore, leaderLevel, leaderAlignment.Full);
            AssertCharacter(cohort);
            Assert.That(cohort.Class.Level, Is.AtMost(leaderLevel - 2));

            var allowedAlignments = CollectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, leaderAlignment.Full);
            Assert.That(allowedAlignments, Contains.Item(cohort.Alignment.Full));
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
            Assert.That(character.Ability.Feats, Is.Not.Empty);

            Assert.That(character.Equipment.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(character.Equipment.PrimaryHand.Name, Is.Not.Empty);
            Assert.That(character.Equipment.Treasure, Is.Not.Null);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty);

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
        public void StressFollower()
        {
            Stress(AssertFollower);
        }

        public void AssertFollower()
        {
            var leaderAlignment = GetNewAlignment();
            var followerLevel = Random.Next(1, 7);

            var follower = LeadershipGenerator.GenerateFollower(followerLevel, leaderAlignment.Full);
            AssertCharacter(follower);
            Assert.That(follower.Class.Level, Is.AtMost(followerLevel));

            var allowedAlignments = CollectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, leaderAlignment.Full);
            Assert.That(allowedAlignments, Contains.Item(follower.Alignment.Full));
        }
    }
}