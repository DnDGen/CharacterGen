using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
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
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Mock<ILevelAdjustmentsProvider> mockLevelAdjustmentsProvider;

        private Alignment alignment;
        private CharacterClassPrototype characterClassPrototype;

        [SetUp]
        public void Setup()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockAlignmentRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(new[] { new Alignment() });

            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(new[] { "class name" });

            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockLevelRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(new[] { 1 });

            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockBaseRaceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(new[] { "base race" });

            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockMetaraceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(new[] { "metarace" });

            var adjustments = new Dictionary<String, Int32>();
            adjustments.Add("base race", 0);
            adjustments.Add("metarace", 0);
            mockLevelAdjustmentsProvider = new Mock<ILevelAdjustmentsProvider>();
            mockLevelAdjustmentsProvider.Setup(p => p.GetLevelAdjustments()).Returns(adjustments);

            verifier = new RandomizerVerifier(mockLevelAdjustmentsProvider.Object);

            alignment = new Alignment();
            characterClassPrototype = new CharacterClassPrototype();
            characterClassPrototype.Level = 1;
        }

        [Test]
        public void RandomizersVerifiedIfAllRandomizersAreCompatible()
        {
            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void RandomizersNotVerifiedIfNoAlignments()
        {
            mockAlignmentRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(Enumerable.Empty<Alignment>());
            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersNotVerifiedIfNoClassNamesForAnyAlignment()
        {
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(Enumerable.Empty<String>());
            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersVerifiedIfAtLeastOneClassNameForAnAlignment()
        {
            var alignments = new[] { new Alignment(), new Alignment(), new Alignment() };
            mockAlignmentRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(alignments);

            mockClassNameRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(Enumerable.Empty<String>())
                .Returns(Enumerable.Empty<String>()).Returns(new[] { "class name" });

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void RandomizersNotVerifiedIfNoLevels()
        {
            mockLevelRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(Enumerable.Empty<Int32>());
            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersNotVerifiedIfNoBaseRacesForAnyCharacterClass()
        {
            mockBaseRaceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>());

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersVerifiedIfAtLeastOneBaseRaceForACharacterClass()
        {
            var classNames = new[] { "first class name", "second class name", "third class name" };
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(classNames);

            mockBaseRaceRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>()).Returns(Enumerable.Empty<String>()).Returns(new[] { "base race" });

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void RandomizersNotVerifiedIfNoMetaracesForAnyCharacterClass()
        {
            mockMetaraceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>());

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersVerifiedIfAtLeastOneMetaraceForACharacterClass()
        {
            var classNames = new[] { "first class name", "second class name", "third class name" };
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(classNames);

            mockMetaraceRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>()).Returns(Enumerable.Empty<String>()).Returns(new[] { "metarace" });

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void RandomizersNotVerifiedIfSumOfLevelAdjustmentsIsNotLessThanLevel()
        {
            var results = new Dictionary<String, Int32>();
            results.Add("base race", 1);
            results.Add("metarace", 1);
            mockLevelAdjustmentsProvider.Setup(p => p.GetLevelAdjustments()).Returns(results);
            mockLevelRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(new[] { 2 });

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void AlignmentNotVerifiedIfNoClassNamesForAnyAlignment()
        {
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(Enumerable.Empty<String>());

            var verified = verifier.VerifyAlignmentCompatibility(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void AlignmentVerifiedIfAtLeastOneClassNameForAnAlignment()
        {
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(new[] { "class name" });

            var verified = verifier.VerifyAlignmentCompatibility(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void AlignmentNotVerifiedIfNoLevels()
        {
            mockLevelRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(Enumerable.Empty<Int32>());
            var verified = verifier.VerifyAlignmentCompatibility(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void AlignmentNotVerifiedIfNoBaseRacesForAnyCharacterClass()
        {
            mockBaseRaceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>());

            var verified = verifier.VerifyAlignmentCompatibility(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void AlignmentVerifiedIfAtLeastOneBaseRaceForACharacterClass()
        {
            var classNames = new[] { "first class name", "second class name", "third class name" };
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(classNames);

            mockBaseRaceRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>()).Returns(Enumerable.Empty<String>()).Returns(new[] { "base race" });

            var verified = verifier.VerifyAlignmentCompatibility(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void AlignmentNotVerifiedIfNoMetaracesForAnyCharacterClass()
        {
            mockMetaraceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>());

            var verified = verifier.VerifyAlignmentCompatibility(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void AlignmentVerifiedIfAtLeastOneMetaraceForACharacterClass()
        {
            var classNames = new[] { "first class name", "second class name", "third class name" };
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(classNames);

            mockMetaraceRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>()).Returns(Enumerable.Empty<String>()).Returns(new[] { "metarace" });

            var verified = verifier.VerifyAlignmentCompatibility(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void AlignmentNotVerifiedIfSumOfLevelAdjustmentsIsNotLessThanLevel()
        {
            var results = new Dictionary<String, Int32>();
            results.Add("base race", 1);
            results.Add("metarace", 1);
            mockLevelAdjustmentsProvider.Setup(p => p.GetLevelAdjustments()).Returns(results);
            mockLevelRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(new[] { 2 });

            var verified = verifier.VerifyAlignmentCompatibility(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void CharacterClassNotVerifiedIfNoBaseRacesForAnyCharacterClass()
        {
            mockBaseRaceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>());

            var verified = verifier.VerifyCharacterClassCompatibility(String.Empty, characterClassPrototype, mockBaseRaceRandomizer.Object, 
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void CharacterClassVerifiedIfAtLeastOneBaseRaceForACharacterClass()
        {
            mockBaseRaceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(new[] { "base race" });

            var verified = verifier.VerifyCharacterClassCompatibility(String.Empty, characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void CharacterClassNotVerifiedIfNoMetaracesForAnyCharacterClass()
        {
            mockMetaraceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>());

            var verified = verifier.VerifyCharacterClassCompatibility(String.Empty, characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void CharacterClassVerifiedIfAtLeastOneMetaraceForACharacterClass()
        {
            mockMetaraceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(new[] { "metarace" });

            var verified = verifier.VerifyCharacterClassCompatibility(String.Empty, characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void CharacterClassNotVerifiedIfSumOfLevelAdjustmentsIsNotLessThanLevel()
        {
            var results = new Dictionary<String, Int32>();
            results.Add("base race", 1);
            results.Add("metarace", 1);
            mockLevelAdjustmentsProvider.Setup(p => p.GetLevelAdjustments()).Returns(results);
            mockLevelRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(new[] { 2 });

            var verified = verifier.VerifyCharacterClassCompatibility(String.Empty, characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }
    }
}