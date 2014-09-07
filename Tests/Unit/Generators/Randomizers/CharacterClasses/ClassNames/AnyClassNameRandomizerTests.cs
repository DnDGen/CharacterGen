using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Generators.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class AnyClassNameRandomizerTests : ClassNameRandomizerTests
    {
        protected override IEnumerable<String> classNamesInGroup
        {
            get { return Enumerable.Empty<String>(); }
        }

        protected override String classNameGroup
        {
            get { return String.Empty; }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new AnyClassNameRandomizer(mockPercentileResultSelector.Object, mockCollectionsSelector.Object);
        }

        [Test]
        public void ClassIsAllowed()
        {
            alignmentClasses.Add(ClassName);
            alignmentClasses.Add(OtherClassName);
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Contains.Item(ClassName));
        }

        [Test]
        public void ClassIsNotAllowed()
        {
            alignmentClasses.Add(OtherClassName);
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Is.Not.Contains(ClassName));
        }
    }
}