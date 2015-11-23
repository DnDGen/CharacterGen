using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Domain.Verifiers;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Verifiers
{
    [TestFixture]
    public class RandomizerVerifierTests
    {
        private IRandomizerVerifier verifier;
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<RaceRandomizer> mockBaseRaceRandomizer;
        private Mock<RaceRandomizer> mockMetaraceRandomizer;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;

        private CharacterClass characterClass;
        private List<Alignment> alignments;
        private List<String> classNames;
        private List<Int32> levels;
        private List<String> baseRaces;
        private List<String> metaraces;
        private Dictionary<String, Int32> adjustments;
        private Alignment alignment;
        private List<String> spellcasters;

        [SetUp]
        public void Setup()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockBaseRaceRandomizer = new Mock<RaceRandomizer>();
            mockMetaraceRandomizer = new Mock<RaceRandomizer>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            verifier = new RandomizerVerifier(mockAdjustmentsSelector.Object, mockCollectionsSelector.Object);

            alignments = new List<Alignment>();
            characterClass = new CharacterClass();
            classNames = new List<String>();
            levels = new List<Int32>();
            baseRaces = new List<String>();
            metaraces = new List<String>();
            adjustments = new Dictionary<String, Int32>();
            alignment = new Alignment();
            spellcasters = new List<String>();

            mockAlignmentRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(alignments);
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(classNames);
            mockLevelRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(levels);
            mockBaseRaceRandomizer.Setup(r => r.GetAllPossible(It.IsAny<Alignment>(), It.IsAny<CharacterClass>()))
                .Returns(baseRaces);
            mockMetaraceRandomizer.Setup(r => r.GetAllPossible(It.IsAny<Alignment>(), It.IsAny<CharacterClass>()))
                .Returns(metaraces);
            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(adjustments);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters))
                .Returns(spellcasters);

            alignments.Add(alignment);
            characterClass.Level = 1;
            characterClass.ClassName = "class name";
            classNames.Add(characterClass.ClassName);
            levels.Add(1);
            baseRaces.Add("base race");
            metaraces.Add(String.Empty);
            adjustments[baseRaces[0]] = 0;
            adjustments[metaraces[0]] = 0;
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

            mockBaseRaceRandomizer.SetupSequence(r => r.GetAllPossible(It.IsAny<Alignment>(), It.IsAny<CharacterClass>()))
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

            mockMetaraceRandomizer.SetupSequence(r => r.GetAllPossible(It.IsAny<Alignment>(), It.IsAny<CharacterClass>()))
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
        public void RandomizersNotVerifiedIfSetLevelAndSetMetarace()
        {
            metaraces.Clear();
            metaraces.Add("metarace");

            adjustments[metaraces[0]] = 2;
            levels.Clear();
            levels.Add(1);

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersNotVerifiedIfNoSpellcastersForLich()
        {
            metaraces.Clear();
            metaraces.Add(RaceConstants.Metaraces.Lich);

            spellcasters.Add("other class name");

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void RandomizersVerifiedIfSpellcastersForLich()
        {
            metaraces.Clear();
            metaraces.Add(RaceConstants.Metaraces.Lich);
            adjustments[RaceConstants.Metaraces.Lich] = 0;

            spellcasters.Add("other class name");
            spellcasters.Add(classNames[0]);

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void RandomizersVerifiedIfMetaracesOtherThanLich()
        {
            metaraces.Add(RaceConstants.Metaraces.Lich);
            spellcasters.Add("other class name");

            var verified = verifier.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
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

            mockBaseRaceRandomizer.SetupSequence(r => r.GetAllPossible(It.IsAny<Alignment>(), It.IsAny<CharacterClass>()))
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

            mockMetaraceRandomizer.SetupSequence(r => r.GetAllPossible(It.IsAny<Alignment>(), It.IsAny<CharacterClass>()))
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

            var verified = verifier.VerifyCharacterClassCompatibility(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void CharacterClassVerifiedIfAtLeastOneBaseRaceForACharacterClass()
        {
            var verified = verifier.VerifyCharacterClassCompatibility(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void CharacterClassNotVerifiedIfNoMetaracesForAnyCharacterClass()
        {
            metaraces.Clear();

            var verified = verifier.VerifyCharacterClassCompatibility(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void CharacterClassNotVerifiedIfNotSpellcasterAndOnlyLichAvailable()
        {
            metaraces.Clear();
            metaraces.Add(RaceConstants.Metaraces.Lich);

            var verified = verifier.VerifyCharacterClassCompatibility(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }

        [Test]
        public void CharacterClassVerifiedIfSpellcasterAndOnlyLichAvailable()
        {
            metaraces.Clear();
            metaraces.Add(RaceConstants.Metaraces.Lich);
            adjustments[RaceConstants.Metaraces.Lich] = 0;
            spellcasters.Add(characterClass.ClassName);

            var verified = verifier.VerifyCharacterClassCompatibility(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void CharacterClassVerifiedIfNotSpellcasterAndMoreThanLichAvailable()
        {
            metaraces.Add(RaceConstants.Metaraces.Lich);

            var verified = verifier.VerifyCharacterClassCompatibility(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.True);
        }

        [Test]
        public void CharacterClassVerifiedIfAtLeastOneMetaraceForACharacterClass()
        {
            var verified = verifier.VerifyCharacterClassCompatibility(alignment, characterClass, mockBaseRaceRandomizer.Object,
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

            var verified = verifier.VerifyCharacterClassCompatibility(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            Assert.That(verified, Is.False);
        }
    }
}