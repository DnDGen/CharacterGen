using CharacterGen.Common;
using CharacterGen.Common.Abilities.Feats;
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
        public override void Stress(string stressSubject)
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
            var leaderClass = GetNewCharacterClass(leaderAlignment);
            leaderClass.Level = Random.Next(6, 21);
            var cohortScore = Random.Next(3, 26);

            var cohort = LeadershipGenerator.GenerateCohort(cohortScore, leaderClass.Level, leaderAlignment.Full, leaderClass.ClassName);
            AssertCharacter(cohort);
            Assert.That(cohort.Equipment.Treasure.Items, Is.Not.Empty);
            Assert.That(cohort.Class.Level, Is.InRange(1, leaderClass.Level - 2));

            var playerCharacters = CollectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Players);
            Assert.That(playerCharacters, Contains.Item(cohort.Class.ClassName));

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
        public void StressNPCCohort()
        {
            Stress(AssertNPCCohort);
        }

        public void AssertNPCCohort()
        {
            var leaderAlignment = GetNewAlignment();
            var leaderClass = CollectionsSelector.SelectRandomFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);
            var leaderLevel = Random.Next(6, 21);
            var cohortScore = Random.Next(3, 26);

            var cohort = LeadershipGenerator.GenerateCohort(cohortScore, leaderLevel, leaderAlignment.Full, leaderClass);
            AssertCharacter(cohort);
            Assert.That(cohort.Class.Level, Is.InRange(1, leaderLevel - 2));

            var npcsCharacters = CollectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);
            Assert.That(npcsCharacters, Contains.Item(cohort.Class.ClassName));

            var allowedAlignments = CollectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, leaderAlignment.Full);
            Assert.That(allowedAlignments, Contains.Item(cohort.Alignment.Full));
        }

        [Test]
        public void StressFollower()
        {
            Stress(AssertFollower);
        }

        public void AssertFollower()
        {
            var leaderAlignment = GetNewAlignment();
            var leaderClass = GetNewCharacterClass(leaderAlignment);
            var followerLevel = Random.Next(1, 7);

            var follower = LeadershipGenerator.GenerateFollower(followerLevel, leaderAlignment.Full, leaderClass.ClassName);
            AssertCharacter(follower);
            Assert.That(follower.Equipment.Treasure.Items, Is.Not.Empty);
            Assert.That(follower.Class.Level, Is.InRange(1, followerLevel));

            var playerCharacters = CollectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Players);
            Assert.That(playerCharacters, Contains.Item(follower.Class.ClassName));

            var allowedAlignments = CollectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, leaderAlignment.Full);
            Assert.That(allowedAlignments, Contains.Item(follower.Alignment.Full));
        }

        [Test]
        public void StressNPCFollower()
        {
            Stress(AssertNPCFollower);
        }

        public void AssertNPCFollower()
        {
            var leaderAlignment = GetNewAlignment();
            var leaderClass = CollectionsSelector.SelectRandomFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);
            var followerLevel = Random.Next(1, 7);

            var follower = LeadershipGenerator.GenerateFollower(followerLevel, leaderAlignment.Full, leaderClass);
            AssertCharacter(follower);
            Assert.That(follower.Class.Level, Is.InRange(1, followerLevel));

            var npcsCharacters = CollectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);
            Assert.That(npcsCharacters, Contains.Item(follower.Class.ClassName));

            var allowedAlignments = CollectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, leaderAlignment.Full);
            Assert.That(allowedAlignments, Contains.Item(follower.Alignment.Full));
        }
    }
}
