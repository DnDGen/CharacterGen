using Ninject;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryTests : DurationTest
    {
        [Inject]
        public ICharacterFactory CharacterFactory { get; set; }
        [Inject]
        public IAlignmentRandomizer AlignmentRandomizer { get; set; }
        [Inject]
        public IClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public ILevelRandomizer LevelRandomizer { get; set; }
        [Inject]
        public IBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public IMetaraceRandomizer MetaraceRandomizer { get; set; }
        [Inject]
        public IStatsRandomizer StatsRandomizer { get; set; }

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
            CharacterFactory.CreateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer,
                BaseRaceRandomizer, MetaraceRandomizer, StatsRandomizer);
            AssertDuration();
        }
    }
}