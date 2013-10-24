using System;
using System.Linq;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
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
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;

        [SetUp]
        public void Setup()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockAlignmentRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(new[] { new Alignment() });

            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(new[] { "class name" });

            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockBaseRaceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<String>())).Returns(new[] { "base race" });

            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockMetaraceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<String>())).Returns(new[] { "metarace" });

            verifier = new RandomizerVerifier(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
        }

        [Test]
        public void VerifiedIfAllRandomizersAreCompatible()
        {
            var verified = verifier.VerifyCompatibility();

            Assert.That(verified, Is.True);
        }

        [Test]
        public void NotVerifiedIfNoAlignments()
        {
            mockAlignmentRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(Enumerable.Empty<Alignment>());

            var verified = verifier.VerifyCompatibility();

            Assert.That(verified, Is.False);
        }

        [Test]
        public void NotVerifiedIfNoClassNamesForAnyAlignment()
        {
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(Enumerable.Empty<String>());

            var verified = verifier.VerifyCompatibility();

            Assert.That(verified, Is.False);
        }

        [Test]
        public void VerifiedIfAtLeastOneClassNameForAnAlignment()
        {
            var alignments = new[] { new Alignment(), new Alignment(), new Alignment() };
            mockAlignmentRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(alignments);

            mockClassNameRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(Enumerable.Empty<String>())
                .Returns(Enumerable.Empty<String>()).Returns(new[] { "class name" });

            var verified = verifier.VerifyCompatibility();

            Assert.That(verified, Is.True);
        }

        [Test]
        public void NotVerifiedIfNoBaseRacesForAnyClassName()
        {
            mockBaseRaceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<String>())).Returns(Enumerable.Empty<String>());

            var verified = verifier.VerifyCompatibility();

            Assert.That(verified, Is.False);
        }

        [Test]
        public void VerifiedIfAtLeastOneBaseRaceForAClassName()
        {
            var classNames = new[] { "first class name", "second class name", "third class name" };
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(classNames);

            mockBaseRaceRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<String>()))
                .Returns(Enumerable.Empty<String>()).Returns(Enumerable.Empty<String>()).Returns(new[] { "base race" });

            var verified = verifier.VerifyCompatibility();

            Assert.That(verified, Is.True);
        }

        [Test]
        public void NotVerifiedIfNoMetaracesForAnyClassName()
        {
            mockMetaraceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<String>())).Returns(Enumerable.Empty<String>());

            var verified = verifier.VerifyCompatibility();

            Assert.That(verified, Is.False);
        }

        [Test]
        public void VerifiedIfAtLeastOneMetaraceForAClassName()
        {
            var classNames = new[] { "first class name", "second class name", "third class name" };
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(classNames);

            mockMetaraceRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<String>()))
                .Returns(Enumerable.Empty<String>()).Returns(Enumerable.Empty<String>()).Returns(new[] { "metarace" });

            var verified = verifier.VerifyCompatibility();

            Assert.That(verified, Is.True);
        }
    }
}