using Ninject;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class HitPointsGeneratorTests : StressTests
    {
        [Inject]
        public IHitPointsGenerator HitPointsGenerator { get; set; }
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }

        protected override void MakeAssertions()
        {
            var dependentData = GetNewDependentData();
            var stats = StatsGenerator.CreateWith(StatsRandomizer, dependentData.CharacterClass, dependentData.Race);

            var hitPoints = HitPointsGenerator.CreateWith(dependentData.CharacterClass, stats[StatConstants.Constitution].Bonus, dependentData.Race);
            Assert.That(hitPoints, Is.AtLeast(dependentData.CharacterClass.Level));
        }
    }
}