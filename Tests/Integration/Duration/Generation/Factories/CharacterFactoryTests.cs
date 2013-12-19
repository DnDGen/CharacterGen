using Ninject;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryTests : DurationTest
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

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void CharacterGeneration()
        {
            CharacterFactory.CreateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                    MetaraceRandomizer, StatsRandomizer);
            AssertDuration();
        }
    }
}