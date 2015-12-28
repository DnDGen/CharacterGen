using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests
    {
        private ISetClassNameRandomizer randomizer;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Alignment alignment;
        private List<string> alignmentClasses;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            randomizer = new SetClassNameRandomizer(mockCollectionsSelector.Object);
            alignment = new Alignment();
            alignmentClasses = new List<string>();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, alignment.Full)).Returns(alignmentClasses);
        }

        [Test]
        public void SetClassNameRandomizerIsAClassNameRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<IClassNameRandomizer>());
        }

        [Test]
        public void ReturnSetClassName()
        {
            randomizer.SetClassName = "class name";
            alignmentClasses.Add("other class name");
            alignmentClasses.Add("class name");

            var className = randomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo("class name"));
        }

        [Test]
        public void ReturnJustSetClassName()
        {
            randomizer.SetClassName = "class name";
            alignmentClasses.Add("other class name");
            alignmentClasses.Add("class name");

            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Contains.Item("class name"));
            Assert.That(classNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ThrowExceptionIfAlignmentDoesNotMatchClass()
        {
            randomizer.SetClassName = "class name";
            alignmentClasses.Add("other class name");

            Assert.That(() => randomizer.Randomize(alignment), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ReturnEmptyCollectionIfAlignmentDoesNotMatchClass()
        {
            randomizer.SetClassName = "class name";
            alignmentClasses.Add("other class name");

            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Is.Empty);
        }
    }
}