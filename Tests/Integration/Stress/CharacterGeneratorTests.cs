using CharacterGen.Common;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.Races;
using CharacterGen.Generators;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
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

            Assert.That(character.Class.ClassName, Is.Not.Empty);
            Assert.That(character.Class.Level, Is.Positive);
            Assert.That(character.Class.ProhibitedFields, Is.Not.Null);
            Assert.That(character.Class.SpecialistFields, Is.Not.Null);

            Assert.That(character.InterestingTrait, Is.Not.Null);

            Assert.That(character.Race.BaseRace, Is.Not.Empty);
            Assert.That(character.Race.Metarace, Is.Not.Null);
            Assert.That(character.Race.AerialSpeed, Is.Not.Negative);

            if (character.Race.HasWings)
            {
                Assert.That(character.Race.AerialSpeed, Is.Positive);
                Assert.That(character.Race.AerialSpeed % 10, Is.EqualTo(0));
            }

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

            foreach (var spellLevel in character.Magic.SpellsPerDay.Keys)
                Assert.That(character.Magic.SpellsPerDay[spellLevel], Is.Positive, spellLevel.ToString());

            Assert.That(character.Combat.BaseAttack.Bonus, Is.Not.Negative);
            Assert.That(character.Combat.HitPoints, Is.AtLeast(character.Class.Level));
            Assert.That(character.Combat.ArmorClass.Full, Is.Positive);
            Assert.That(character.Combat.ArmorClass.FlatFooted, Is.Positive);
            Assert.That(character.Combat.ArmorClass.Touch, Is.Positive);

            Assert.That(character.Leadership, Is.Not.Null);
        }

        [Test]
        public void LeadershipHappens()
        {
            var character = Generate<Character>(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => c.Leadership.Score > 0);

            Assert.That(character.Leadership.Score, Is.Positive);
            Assert.That(character.Leadership.Cohort, Is.Not.Null);
        }

        [Test]
        public void LeadershipWithoutFollowersHappens()
        {
            var character = Generate<Character>(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => c.Leadership.Score > 0 && c.Leadership.Followers.Any() == false);

            Assert.That(character.Leadership.Score, Is.Positive);
            Assert.That(character.Leadership.Cohort, Is.Not.Null);
            Assert.That(character.Leadership.Followers, Is.Empty);
        }

        [Test]
        public void LeadershipWithFollowersHappens()
        {
            var character = Generate(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => c.Leadership.Score > 0 && c.Leadership.Followers.Any());

            Assert.That(character.Leadership.Score, Is.Positive);
            Assert.That(character.Leadership.Cohort, Is.Not.Null);
            Assert.That(character.Leadership.Followers, Is.Not.Empty);
        }

        [Test]
        public void LeadershipDoesNotHappen()
        {
            var character = Generate(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer),
                c => c.Leadership.Score == 0);

            Assert.That(character.Leadership.Score, Is.EqualTo(0));
            Assert.That(character.Leadership.Cohort, Is.Null);
            Assert.That(character.Leadership.Followers, Is.Empty);
        }
    }
}