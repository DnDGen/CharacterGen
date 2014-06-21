using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class CharacterClassGeneratorTests : StressTests
    {
        private IEnumerable<String> classNames;

        [SetUp]
        public void Setup()
        {
            classNames = CharacterClassConstants.GetClassNames();
        }

        protected override void MakeAssertions()
        {
            var dependentData = GetNewDependentData();

            var prototype = CharacterClassGenerator.GeneratePrototypeWith(dependentData.Alignment, LevelRandomizer, ClassNameRandomizer);
            Assert.That(classNames, Contains.Item(prototype.ClassName));
            Assert.That(prototype.Level, Is.Positive);

            var characterClass = CharacterClassGenerator.GenerateWith(prototype);
            Assert.That(characterClass.ClassName, Is.EqualTo(prototype.ClassName));
            Assert.That(characterClass.Level, Is.EqualTo(prototype.Level));
            Assert.That(characterClass.BaseAttack.Bonus, Is.Not.Negative);
        }
    }
}