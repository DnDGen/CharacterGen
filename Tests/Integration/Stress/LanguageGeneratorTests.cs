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
            var alignment = GetNewAlignment();
            var prototype = GetNewCharacterClassPrototype(alignment);
            var characterClass = GetNewCharacterClass(prototype);
            var race = GetNewRace(alignment, prototype);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);

            var languages = LanguageGenerator.GenerateWith(race, characterClass.ClassName, stats[StatConstants.Intelligence].Bonus);
            Assert.That(languages, Is.Not.Empty);
        }
    }
}