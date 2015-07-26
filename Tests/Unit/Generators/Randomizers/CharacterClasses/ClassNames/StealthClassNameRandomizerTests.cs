﻿using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class StealthClassNameRandomizerTests : ClassNameRandomizerTests
    {
        protected override String classNameGroup
        {
            get { return "Stealth"; }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new StealthClassNameRandomizer(mockPercentileResultSelector.Object, mockCollectionsSelector.Object);
        }

        [Test]
        public void ClassIsAllowed()
        {
            alignmentClasses.Add(ClassName);
            groupClasses.Add(ClassName);
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Contains.Item(ClassName));
        }

        [Test]
        public void ClassIsNotInAlignment()
        {
            groupClasses.Add(ClassName);
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Is.Not.Contains(ClassName));
        }

        [Test]
        public void ClassIsNotInGroup()
        {
            alignmentClasses.Add(ClassName);
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Is.Not.Contains(ClassName));
        }
    }
}