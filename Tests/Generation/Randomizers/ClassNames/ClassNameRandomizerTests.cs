using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.ClassNames
{
    [TestFixture]
    public class ClassNameRandomizerTests
    {
        protected Mock<IPercentileResultProvider> mockPercentileResultProvider;
        protected Alignment alignment;

        protected IClassNameRandomizer randomizer;
        protected String controlCase;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            alignment = new Alignment();
        }

        protected void AssertControlIsAllowed(String secondaryControl)
        {
            var result = GetResult(controlCase, secondaryControl);
            Assert.That(result, Is.EqualTo(controlCase));
        }

        protected void AssertClassIsAllowed(String className)
        {
            var result = GetResult(className, controlCase);
            Assert.That(result, Is.EqualTo(className));
        }

        protected void AssertClassIsNotAllowed(String className)
        {
            var result = GetResult(className, controlCase);
            Assert.That(result, Is.EqualTo(controlCase));
        }

        private String GetResult(String testCase, String controlCase)
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(testCase)
                .Returns(controlCase);

            return randomizer.Randomize(alignment);
        }
    }
}