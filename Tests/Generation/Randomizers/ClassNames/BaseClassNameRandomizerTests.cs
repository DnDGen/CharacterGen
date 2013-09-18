using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Providers.Interfaces;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Randomizers.ClassNames
{
    [TestFixture]
    public class BaseClassNameRandomizerTests
    {
        private TestClassRandomizer characterClassRandomzier;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            alignment.Goodness = AlignmentConstants.Good;

            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            characterClassRandomzier = new TestClassRandomizer(mockPercentileResultProvider.Object);
        }

        [Test]
        public void LoopUntilCharacterClassIsAllowed()
        {
            characterClassRandomzier.ClassIsAllowed = false;
            characterClassRandomzier.Randomize(alignment);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("GoodCharacterClasses"), Times.Exactly(2));
        }

        [Test]
        public void ReturnCharacterClassFromPercentileResultProvider()
        {
            characterClassRandomzier.ClassIsAllowed = true;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("GoodCharacterClasses")).Returns("result");

            var result = characterClassRandomzier.Randomize(alignment);
            Assert.That(result, Is.EqualTo("result"));
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