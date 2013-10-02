using System;
using Moq;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Verifiers.Alignments
{
    [TestFixture]
    public class AlignmentVerifierTests
    {
        protected IAlignmentVerifier verifier;
        protected Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
        }

        protected void AssertRandomizerIsAllowed(IClassNameRandomizer randomizer)
        {
            var allowed = verifier.VerifyCompatibility(randomizer);
            Assert.That(allowed, Is.True);
        }

        protected void AssertRandomizerIsNotAllowed(IClassNameRandomizer randomizer)
        {
            var allowed = verifier.VerifyCompatibility(randomizer);
            Assert.That(allowed, Is.False);
        }

        protected void AssertRandomizerIsAllowed(IBaseRaceRandomizer randomizer)
        {
            var allowed = verifier.VerifyCompatibility(randomizer);
            Assert.That(allowed, Is.True);
        }

        protected void AssertRandomizerIsNotAllowed(IBaseRaceRandomizer randomizer)
        {
            var allowed = verifier.VerifyCompatibility(randomizer);
            Assert.That(allowed, Is.False);
        }

        protected void AssertClassNameIsAllowed(String className)
        {
            var allowed = GetAllowed(className);
            Assert.That(allowed, Is.True);
        }

        protected void AssertClassNameIsNotAllowed(String className)
        {
            var allowed = GetAllowed(className);
            Assert.That(allowed, Is.False);
        }

        private Boolean GetAllowed(String className)
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = className;

            return verifier.VerifyCompatibility(randomizer);
        }
    }
}