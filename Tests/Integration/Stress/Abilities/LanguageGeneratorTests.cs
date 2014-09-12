using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Abilities;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Abilities
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

        private IEnumerable<String> allLanguages;

        [SetUp]
        public void Setup()
        {
            allLanguages = LanguageConstants.GetLanguages();
        }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }

        private void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);

            var languages = LanguageGenerator.GenerateWith(race, characterClass.ClassName, stats[StatConstants.Intelligence].Bonus);
            Assert.That(languages, Is.Not.Empty);

            foreach (var language in languages)
                Assert.That(allLanguages, Contains.Item(language));
        }
    }
}