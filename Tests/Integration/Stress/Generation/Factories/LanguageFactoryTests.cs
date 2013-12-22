using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Factories
{
    [TestFixture]
    public class LanguageFactoryTests : StressTest
    {
        [Inject]
        public ILanguageFactory LanguageFactory { get; set; }

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