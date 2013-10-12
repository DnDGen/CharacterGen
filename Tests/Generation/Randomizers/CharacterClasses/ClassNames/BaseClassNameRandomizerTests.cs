using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class BaseClassNameRandomizerTests
    {
        private TestClassRandomizer randomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            alignment.Goodness = AlignmentConstants.Good;

            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            randomizer = new TestClassRandomizer(mockPercentileResultProvider.Object);
            randomizer.ClassIsAllowed = true;
        }

        [Test]
        public void LoopUntilCharacterClassIsAllowed()
        {
            randomizer.ClassIsAllowed = false;
            randomizer.Randomize(alignment);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("GoodCharacterClasses"), Times.Exactly(2));
        }

        [Test]
        public void ReturnCharacterClassFromPercentileResultProvider()
        {
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("GoodCharacterClasses")).Returns("result");

            var result = randomizer.Randomize(alignment);
            Assert.That(result, Is.EqualTo("result"));
        }

        [Test]
        public void AccessesTableAlignmentGoodnessClassNames()
        {
            var result = randomizer.Randomize(alignment);
            var tableName = String.Format("{0}CharacterClasses", alignment.GetGoodnessString());
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(tableName), Times.Once());
        }

        private class TestClassRandomizer : BaseClassNameRandomizer
        {
            public Boolean ClassIsAllowed { get; set; }

            public TestClassRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

            protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
            {
                var toReturn = ClassIsAllowed;
                ClassIsAllowed = !ClassIsAllowed;
                return toReturn;
            }
        }
    }
}