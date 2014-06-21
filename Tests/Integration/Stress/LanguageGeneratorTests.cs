using Ninject;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class LanguageGeneratorTests : StressTests
    {
        [Inject]
        public ILanguageGenerator LanguageGenerator { get; set; }
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }

        protected override void MakeAssertions()
        {
            var dependentData = GetNewDependentData();
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, dependentData.CharacterClass, dependentData.Race);

            var languages = LanguageGenerator.GenerateWith(dependentData.Race, dependentData.CharacterClass.ClassName, stats[StatConstants.Intelligence].Bonus);
            Assert.That(languages, Is.Not.Empty);
        }
    }
}