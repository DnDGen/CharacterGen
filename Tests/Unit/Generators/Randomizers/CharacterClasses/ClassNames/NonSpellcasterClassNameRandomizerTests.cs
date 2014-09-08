using System;
using NPCGen.Generators.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class NonSpellcasterClassNameRandomizerTests : ClassNameRandomizerTests
    {
        protected override String classNameGroup
        {
            get { return "Spellcasters"; }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new NonSpellcasterClassNameRandomizer(mockPercentileResultSelector.Object, mockCollectionsSelector.Object);
        }

        [Test]
        public void ClassIsAllowed()
        {
            alignmentClasses.Add(ClassName);
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Contains.Item(ClassName));
        }

        [Test]
        public void ClassIsNotInAlignment()
        {
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Is.Not.Contains(ClassName));
        }

        [Test]
        public void ClassIsInGroup()
        {
            alignmentClasses.Add(ClassName);
            groupClasses.Add(ClassName);
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Is.Not.Contains(ClassName));
        }
    }
}