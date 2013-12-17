using Ninject;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;
using System;
using System.Linq;

namespace NPCGen.Tests.Integration.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryTests : IntegrationTest
    {
        [Inject]
        public ICharacterFactory CharacterFactory { get; set; }
        [Inject]
        public AnyAlignmentRandomizer AlignmentRandomizer { get; set; }
        [Inject]
        public AnyClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public AnyLevelRandomizer LevelRandomizer { get; set; }
        [Inject]
        public AnyBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public AnyMetaraceRandomizer MetaraceRandomizer { get; set; }
        [Inject]
        public RawStatsRandomizer StatsRandomizer { get; set; }

        private Character character;

        [SetUp]
        public void Setup()
        {
            kernel.Inject(this);

            character = CharacterFactory.CreateUsing(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);
        }

        [Test]
        public void CharacterFactoryReturnsACharacter()
        {
            Assert.That(character, Is.Not.Null);
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithAlignment()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            Assert.That(character.Alignment, Is.Not.Null);
            Assert.That(goodnesses.Contains(character.Alignment.Goodness), Is.True);
            Assert.That(lawfulnesses.Contains(character.Alignment.Lawfulness), Is.True);
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithCharacterClass()
        {
            var classes = CharacterClassConstants.GetClassNames();

            Assert.That(character.Class, Is.Not.Null);
            Assert.That(classes.Contains(character.Class.ClassName), Is.True);
            Assert.That(character.Class.Level, Is.GreaterThan(0));
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithFeats()
        {
            Assert.That(character.Feats, Is.Not.Null);
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithHitPointsGreaterThanZero()
        {
            Assert.That(character.HitPoints, Is.GreaterThan(0));
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithLanguages()
        {
            Assert.That(character.Languages, Is.Not.Null);
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithRace()
        {
            Assert.That(character.Race, Is.Not.Null);
            Assert.That(character.Race.BaseRace, Is.Not.EqualTo(String.Empty));
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithSkills()
        {
            Assert.That(character.Skills, Is.Not.Null);
        }

        [Test]
        public void CharacterFactoryReturnsCharacterWithStats()
        {
            Assert.That(character.Stats, Is.Not.Null);

            var statNames = StatConstants.GetStats();
            foreach (var statName in statNames)
            {
                Assert.That(character.Stats.ContainsKey(statName), Is.True);

                var stat = character.Stats[statName];
                Assert.That(stat, Is.Not.Null);
                Assert.That(stat.Value, Is.GreaterThan(0));
            }
        }
    }
}