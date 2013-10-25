using System;
using System.Linq;
using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryIntegrationTests
    {
        private Character character;

        [SetUp]
        public void Setup()
        {
            var random = new Random();
            var dice = DiceFactory.Create(random);
            var streamLoader = new EmbeddedResourceStreamLoader();
            var percentileXmlParser = new PercentileXmlParser(streamLoader);
            var percentileResultProvider = new PercentileResultProvider(percentileXmlParser, dice);
            var alignmentRandomizer = new AnyAlignmentRandomizer(dice, percentileResultProvider);
            var classNameRandomizer = new AnyClassNameRandomizer(percentileResultProvider);
            var levelRandomizer = new AnyLevelRandomizer(dice);
            var baseRaceRandomizer = new AnyBaseRaceRandomizer(percentileResultProvider);
            var metaraceRandomizer = new AnyMetaraceRandomizer(percentileResultProvider);
            var statsRandomizer = new RawStatsRandomizer(dice);

            character = CharacterFactory.CreateUsing(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer,
                metaraceRandomizer, statsRandomizer, dice);
        }

        [Test]
        public void CharacterFactoryReturnsCharacter()
        {
            Assert.That(character, Is.Not.Null);
        }

        [Test]
        public void CharacterFactoryGeneratesAlignment()
        {
            Assert.That(character.Alignment, Is.Not.Null);
            Assert.That(character.Alignment.Goodness, Is.Not.EqualTo(String.Empty));
            Assert.That(character.Alignment.Lawfulness, Is.Not.EqualTo(String.Empty));
        }

        [Test]
        public void CharacterFactoryGeneratesCharacterClass()
        {
            Assert.That(character.Class, Is.Not.Null);
            Assert.That(character.Class.ClassName, Is.Not.EqualTo(String.Empty));
            Assert.That(character.Class.Level, Is.Not.EqualTo(0));
        }

        [Test]
        public void CharacterFactoryGeneratesFeats()
        {
            Assert.That(character.Feats, Is.Not.Null);
            Assert.That(character.Feats.Any(), Is.True);
            Assert.That(character.Feats.All(f => !String.IsNullOrEmpty(f.Description)), Is.True);
            Assert.That(character.Feats.All(f => !String.IsNullOrEmpty(f.Name)), Is.True);
        }

        [Test]
        public void CharacterFactoryGeneratesHitPoints()
        {
            Assert.That(character.HitPoints, Is.GreaterThan(0));
        }

        [Test]
        public void CharacterFactoryGeneratesLanguages()
        {
            Assert.That(character.Languages, Is.Not.Null);
            Assert.That(character.Languages.Any(), Is.True);
            Assert.That(character.Languages.All(l => !String.IsNullOrEmpty(l)), Is.True);
        }

        [Test]
        public void CharacterFactoryGeneratesRace()
        {
            Assert.That(character.Race, Is.Not.Null);
            Assert.That(character.Race.BaseRace, Is.Not.EqualTo(String.Empty));
        }

        [Test]
        public void CharacterFactoryGeneratesSkills()
        {
            Assert.That(character.Skills, Is.Not.Null);
            Assert.That(character.Skills.Any(), Is.True);
            Assert.That(character.Skills.All(s => s.BaseStat != null), Is.True);
            Assert.That(character.Skills.All(s => !String.IsNullOrEmpty(s.Name)), Is.True);
        }

        [Test]
        public void CharacterFactoryGeneratesStats()
        {
            Assert.That(character.Stats, Is.Not.Null);
            Assert.That(character.Stats.ContainsKey(StatConstants.Strength), Is.True);
            Assert.That(character.Stats.ContainsKey(StatConstants.Dexterity), Is.True);
            Assert.That(character.Stats.ContainsKey(StatConstants.Constitution), Is.True);
            Assert.That(character.Stats.ContainsKey(StatConstants.Intelligence), Is.True);
            Assert.That(character.Stats.ContainsKey(StatConstants.Wisdom), Is.True);
            Assert.That(character.Stats.ContainsKey(StatConstants.Charisma), Is.True);
            Assert.That(character.Stats.Count(), Is.EqualTo(6));

            foreach (var stat in character.Stats.Values)
            {
                Assert.That(stat, Is.Not.Null);
                Assert.That(stat.Value, Is.GreaterThan(0));
            }
        }
    }
}