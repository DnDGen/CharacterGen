﻿using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Factories
{
    [TestFixture]
    public class CharacterClassFactoryTests : StressTest
    {
        [Inject]
        public ICharacterClassFactory CharacterClassFactory { get; set; }
        [Inject]
        public IClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public ILevelRandomizer LevelRandomizer { get; set; }

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
        public void CharacterClassFactoryReturnsPrototype()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                Assert.That(prototype, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsPrototypeWithClassName()
        {
            var classNames = CharacterClassConstants.GetClassNames();

            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                Assert.That(classNames.Contains(prototype.ClassName), Is.True);
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsPrototypeWithLevel()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                Assert.That(prototype.Level, Is.GreaterThan(0));
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClass()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClassWithClassNameFromPrototype()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.ClassName, Is.EqualTo(prototype.ClassName));
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClassWithLevelFromPrototype()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.Level, Is.EqualTo(prototype.Level));
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClassWithBaseAttack()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.BaseAttack, Is.Not.Null);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.Not.Negative);
            }

            AssertIterations();
        }
    }
}