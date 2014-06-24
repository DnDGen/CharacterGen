using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Verifiers;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Verifiers
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
        private Mock<ILevelAdjustmentsSelector> mockLevelAdjustmentsSelector;

        private CharacterClassPrototype characterClassPrototype;
        private List<Alignment> alignments;
        private List<String> classNames;
        private List<Int32> levels;
        private List<String> baseRaces;
        private List<String> metaraces;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockLevelAdjustmentsSelector = new Mock<ILevelAdjustmentsSelector>();
            verifier = new RandomizerVerifier(mockLevelAdjustmentsSelector.Object);

            alignments = new List<Alignment>();
            characterClassPrototype = new CharacterClassPrototype();
            classNames = new List<String>();
            levels = new List<Int32>();
            baseRaces = new List<String>();
            metaraces = new List<String>();
            adjustments = new Dictionary<String, Int32>();

            mockAlignmentRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(alignments);
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(classNames);
            mockLevelRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(levels);
            mockBaseRaceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(baseRaces);
            mockMetaraceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(metaraces);
            mockLevelAdjustmentsSelector.Setup(p => p.GetAdjustments()).Returns(adjustments);

            var alignment = new Alignment();
            alignments.Add(alignment);
            characterClassPrototype.Level = 1;
            classNames.Add("class name");
            levels.Add(1);
            baseRaces.Add("base race");
            metaraces.Add(String.Empty);
            adjustments.Add(baseRaces[0], 0);
            adjustments.Add(metaraces[0], 0);
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
            alignments.Clear();
            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersNotVerifiedIfNoClassNamesForAnyAlignment()
        {
            classNames.Clear();
            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersVerifiedIfAtLeastOneClassNameForAnAlignment()
        {
            alignments.Add(new Alignment());
            alignments.Add(new Alignment());

            mockClassNameRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(Enumerable.Empty<String>())
                .Returns(Enumerable.Empty<String>()).Returns(classNames);

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void RandomizersNotVerifiedIfNoLevels()
        {
            levels.Clear();
            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersNotVerifiedIfNoBaseRacesForAnyCharacterClass()
        {
            baseRaces.Clear();

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersVerifiedIfAtLeastOneBaseRaceForACharacterClass()
        {
            classNames.Add("second class name");
            classNames.Add("third class name");

            mockBaseRaceRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>()).Returns(Enumerable.Empty<String>()).Returns(baseRaces);

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void RandomizersNotVerifiedIfNoMetaracesForAnyCharacterClass()
        {
            metaraces.Clear();

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersVerifiedIfAtLeastOneMetaraceForACharacterClass()
        {
            classNames.Add("second class name");
            classNames.Add("third class name");

            mockMetaraceRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>()).Returns(Enumerable.Empty<String>()).Returns(metaraces);

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void RandomizersNotVerifiedIfSumOfLevelAdjustmentsIsNotLessThanLevel()
        {
            adjustments[baseRaces[0]] = 1;
            adjustments[metaraces[0]] = 1;
            levels.Clear();
            levels.Add(2);

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void AlignmentNotVerifiedIfNoClassNamesForAnyAlignment()
        {
            classNames.Clear();

            var verified = verifier.VerifyAlignmentCompatibility(alignments[0], mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void AlignmentVerifiedIfAtLeastOneClassNameForAnAlignment()
        {
            var verified = verifier.VerifyAlignmentCompatibility(alignments[0], mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void AlignmentNotVerifiedIfNoLevels()
        {
            levels.Clear();

            var verified = verifier.VerifyAlignmentCompatibility(alignments[0], mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void AlignmentNotVerifiedIfNoBaseRacesForAnyCharacterClass()
        {
            baseRaces.Clear();

            var verified = verifier.VerifyAlignmentCompatibility(alignments[0], mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void AlignmentVerifiedIfAtLeastOneBaseRaceForACharacterClass()
        {
            classNames.Add("second class name");
            classNames.Add("third class name");

            mockBaseRaceRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>()).Returns(Enumerable.Empty<String>()).Returns(baseRaces);

            var verified = verifier.VerifyAlignmentCompatibility(alignments[0], mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void AlignmentNotVerifiedIfNoMetaracesForAnyCharacterClass()
        {
            metaraces.Clear();

            var verified = verifier.VerifyAlignmentCompatibility(alignments[0], mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void AlignmentVerifiedIfAtLeastOneMetaraceForACharacterClass()
        {
            classNames.Add("second class name");
            classNames.Add("third class name");

            mockMetaraceRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(Enumerable.Empty<String>()).Returns(Enumerable.Empty<String>()).Returns(metaraces);

            var verified = verifier.VerifyAlignmentCompatibility(alignments[0], mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void AlignmentNotVerifiedIfSumOfLevelAdjustmentsIsNotLessThanLevel()
        {
            adjustments[baseRaces[0]] = 1;
            adjustments[metaraces[0]] = 1;
            levels.Clear();
            levels.Add(2);

            var verified = verifier.VerifyAlignmentCompatibility(alignments[0], mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void CharacterClassNotVerifiedIfNoBaseRacesForAnyCharacterClass()
        {
            baseRaces.Clear();

            var verified = verifier.VerifyCharacterClassCompatibility(String.Empty, characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void CharacterClassVerifiedIfAtLeastOneBaseRaceForACharacterClass()
        {
            var verified = verifier.VerifyCharacterClassCompatibility(String.Empty, characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void CharacterClassNotVerifiedIfNoMetaracesForAnyCharacterClass()
        {
            metaraces.Clear();

            var verified = verifier.VerifyCharacterClassCompatibility(String.Empty, characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void CharacterClassVerifiedIfAtLeastOneMetaraceForACharacterClass()
        {
            var verified = verifier.VerifyCharacterClassCompatibility(String.Empty, characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void CharacterClassNotVerifiedIfSumOfLevelAdjustmentsIsNotLessThanLevel()
        {
            adjustments[baseRaces[0]] = 1;
            adjustments[metaraces[0]] = 1;
            levels.Clear();
            levels.Add(2);

            var verified = verifier.VerifyCharacterClassCompatibility(String.Empty, characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }
    }
}