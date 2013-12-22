using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Factories
{
    [TestFixture]
    public class LanguageFactoryTests : DurationTest
    {
        [Inject]
        public ILanguageFactory LanguageFactory { get; set; }
        [Inject]
        public DependentDataCollection DependentData { get; set; }
        [Inject]
        public Dictionary<String, Stat> Stats { get; set; }

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
        public void LanguagesGeneration()
        {
            LanguageFactory.CreateWith(DependentData.Race, DependentData.CharacterClass.ClassName, Stats[StatConstants.Intelligence].Bonus);
            AssertDuration();
        }
    }
}