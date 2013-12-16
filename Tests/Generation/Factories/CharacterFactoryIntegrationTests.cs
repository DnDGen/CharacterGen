using NPCGen.Bootstrap;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryIntegrationTests
    {
        [InjectDependency]
        public ICharacterFactory CharacterFactory { get; set; }
        [InjectDependency]
        public AnyAlignmentRandomizer AlignmentRandomizer { get; set; }
        [InjectDependency]
        public AnyClassNameRandomizer ClassNameRandomizer { get; set; }
        [InjectDependency]
        public AnyLevelRandomizer LevelRandomizer { get; set; }
        [InjectDependency]
        public AnyBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [InjectDependency]
        public AnyMetaraceRandomizer MetaraceRandomizer { get; set; }
        [InjectDependency]
        public RawStatsRandomizer StatsRandomizer { get; set; }

        [Test]
        public void CharacterFactoryReturnsACompleteCharacter()
        {
            var character = CharacterFactory.CreateUsing(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer, StatsRandomizer);

            Assert.That(character, Is.Not.Null);
            Assert.That(character.Alignment, Is.Not.Null);
            Assert.That(character.Class, Is.Not.Null);
            Assert.That(character.Feats, Is.Not.Null);
            Assert.That(character.HitPoints, Is.Not.Null);
            Assert.That(character.Languages, Is.Not.Null);
            Assert.That(character.Race, Is.Not.Null);
            Assert.That(character.Skills, Is.Not.Null);
            Assert.That(character.Stats, Is.Not.Null);
        }
    }
}