﻿using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Factories
{
    [TestFixture]
    public class CharacterClassFactoryTests : DurationTest
    {
        [Inject]
        public ICharacterClassFactory CharacterClassFactory { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }
        [Inject]
        public AnyLevelRandomizer LevelRandomizer { get; set; }
        [Inject]
        public AnyClassNameRandomizer ClassNameRandomizer { get; set; }

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
        public void CharacterClassGeneration()
        {
            CharacterClassFactory.CreatePrototypeWith(Alignment, LevelRandomizer, ClassNameRandomizer);
            AssertDuration();
        }
    }
}