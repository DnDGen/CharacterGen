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
        private Mock<IClassNameVerifier> mockClassNameVerifier;
        private Mock<IBaseRaceVerifier> mockBaseRaceVerifier;

        [SetUp]
        public void Setup()
        {
            mockAlignmentVerifier = new Mock<IAlignmentVerifier>();
            mockClassNameVerifier = new Mock<IClassNameVerifier>();
            mockBaseRaceVerifier = new Mock<IBaseRaceVerifier>();

            mockAlignmentVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IClassNameRandomizer>())).Returns(true);
            mockAlignmentVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IBaseRaceRandomizer>())).Returns(true);
            mockAlignmentVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>())).Returns(true);
            mockClassNameVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IBaseRaceRandomizer>())).Returns(true);
            mockClassNameVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>())).Returns(true);
            mockBaseRaceVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>())).Returns(true);

            verifiers = new VerifierCollection();
            verifiers.AlignmentVerifier = mockAlignmentVerifier.Object;
            verifiers.ClassNameVerifier = mockClassNameVerifier.Object;
            verifiers.BaseRaceVerifier = mockBaseRaceVerifier.Object;

            verifier = new RandomizerVerifier();
        }

        [Test]
        public void RandomizerVerifierVerifiesAlignmentRandomizerAgainstClassNameRandomizer()
        {
            Verify();
            mockAlignmentVerifier.Verify(v => v.VerifyCompatibility(It.IsAny<IClassNameRandomizer>()), Times.Once);
        }

        [Test]
        public void NotVerifiedIfAlignmentRandomizerAndClassNameRandomizerAreIncompatible()
        {
            mockAlignmentVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IClassNameRandomizer>())).Returns(false);

            var verified = Verify();
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizerVerifierVerifiesAlignmentRandomizerAgainstBaseRaceRandomizer()
        {
            Verify();
            mockAlignmentVerifier.Verify(v => v.VerifyCompatibility(It.IsAny<IBaseRaceRandomizer>()), Times.Once);
        }

        [Test]
        public void NotVerifiedIfAlignmentRandomizerAndBaseRaceRandomizerAreIncompatible()
        {
            mockAlignmentVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IBaseRaceRandomizer>())).Returns(false);

            var verified = Verify();
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizerVerifierVerifiesAlignmentRandomizerAgainstMetaraceRandomizer()
        {
            Verify();
            mockAlignmentVerifier.Verify(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>()), Times.Once);
        }

        [Test]
        public void NotVerifiedIfAlignmentRandomizerAndMetaraceRandomizerAreIncompatible()
        {
            mockAlignmentVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>())).Returns(false);

            var verified = Verify();
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizerVerifierVerifiesClassNameRandomizerAgainstBaseRaceRandomizer()
        {
            Verify();
            mockClassNameVerifier.Verify(v => v.VerifyCompatibility(It.IsAny<IBaseRaceRandomizer>()), Times.Once);
        }

        [Test]
        public void NotVerifiedIfClassNameRandomizerAndBaseRaceRandomizerAreIncompatible()
        {
            mockClassNameVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IBaseRaceRandomizer>())).Returns(false);

            var verified = Verify();
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizerVerifierVerifiesClassNameRandomizerAgainstMetaraceRandomizer()
        {
            Verify();
            mockClassNameVerifier.Verify(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>()), Times.Once);
        }

        [Test]
        public void NotVerifiedIfClassNameRandomizerAndMetaraceRandomizerAreIncompatible()
        {
            mockClassNameVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>())).Returns(false);

            var verified = Verify();
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizerVerifierVerifiesBaseRaceRandomizerAgainstMetaraceRandomizer()
        {
            Verify();
            mockBaseRaceVerifier.Verify(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>()), Times.Once);
        }

        [Test]
        public void NotVerifiedIfBaseRaceRandomizerAndMetaraceRandomizerAreIncompatible()
        {
            mockBaseRaceVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>())).Returns(false);

            var verified = Verify();
            Assert.That(verified, Is.False);
        }

        [Test]
        public void VerifiedIfAllRandomizersAreCompatible()
        {
            var verified = Verify();
            Assert.That(verified, Is.True);
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