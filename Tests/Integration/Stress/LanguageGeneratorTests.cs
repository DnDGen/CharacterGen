using System.Linq;
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
        [Inject]
        public IStatsRandomizer StatsRandomizer { get; set; }

        protected override void MakeAssertions()
        {
            var dependentData = GetNewDependentData();
            var stats = StatsGenerator.CreateWith(StatsRandomizer, dependentData.CharacterClass, dependentData.Race);

            var languages = LanguageGenerator.CreateWith(dependentData.Race, dependentData.CharacterClass.ClassName, stats[StatConstants.Intelligence].Bonus);
            Assert.That(languages.Count(), Is.AtLeast(1));
        }
    }
}