using Ninject;
using NPCGen.Core.Generation.Factories.Interfaces;
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
            CharacterFactory.CreateWith(GetAlignmentRandomizer(kernel), GetClassNameRandomizer(kernel), GetLevelRandomizer(kernel),
                GetBaseRaceRandomizer(kernel), GetMetaraceRandomizer(kernel), StatsRandomizer);
            AssertDuration();
        }
    }
}