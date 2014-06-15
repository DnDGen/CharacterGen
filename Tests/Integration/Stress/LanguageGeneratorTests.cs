using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class LanguageGeneratorTests : StressTests
    {
        [Inject]
        public ILanguageGenerator LanguageFactory { get; set; }

        [Test]
        public void LanguageFactoryReturnsLanguages()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var stats = GetNewInstanceOf<Dictionary<String, Stat>>();
                var languages = LanguageFactory.CreateWith(data.Race, data.CharacterClass.ClassName, stats[StatConstants.Intelligence].Bonus);
                Assert.That(languages, Is.Not.Null);
            }

            AssertIterations();
        }
    }
}