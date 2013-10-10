using System;
using Moq;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Verifiers;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Verifiers
{
    [TestFixture]
    public class RandomizerVerifierTests
    {
        private IRandomizerVerifier verifier;
        private VerifierCollection verifiers;
        private Mock<IAlignmentVerifier> mockAlignmentVerifier;

        [SetUp]
        public void Setup()
        {
            mockAlignmentVerifier = new Mock<IAlignmentVerifier>();

            verifiers = new VerifierCollection();
            verifiers.AlignmentVerifier = mockAlignmentVerifier.Object;

            verifier = new RandomizerVerifier();
        }

        [Test]
        public void NotVerifiedIfAlignmentRandomizerAndClassNameRandomizerAreIncompatible()
        {
            mockAlignmentVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IClassNameRandomizer>())).Returns(false);

            var verified = Verify();
            Assert.That(verified, Is.False);
        }

        private Boolean Verify()
        {
            var mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            var mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            var mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();

            return verifier.VerifyCompatibility(verifiers, mockClassNameRandomizer.Object, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
        }
    }
}