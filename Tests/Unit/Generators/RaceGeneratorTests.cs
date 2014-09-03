using System;
using D20Dice;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators
{
    [TestFixture]
    public class RaceGeneratorTests
    {
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Mock<IDice> mockDice;
        private IRaceGenerator raceGenerator;
        private CharacterClass characterClass;
        private Mock<ICollectionsSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            raceGenerator = new RaceGenerator(mockDice.Object, mockCollectionsSelector.Object);

            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            characterClass = new CharacterClass();

            mockDice.Setup(d => d.Roll(1).d2()).Returns(1);
        }

        [Test]
        public void RandomizeBaseRace()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize("goodness", characterClass)).Returns("base race");

            var race = raceGenerator.GenerateWith("goodness", characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.BaseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void RandomizeMetarace()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize("goodness", characterClass)).Returns("metarace");

            var race = raceGenerator.GenerateWith("goodness", characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Metarace, Is.EqualTo("metarace"));
        }

        [Test]
        public void ReturnMaleOnLowRoll()
        {
            mockDice.Setup(d => d.Roll(1).d2()).Returns(1);

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void ReturnFemaleOnHighRoll()
        {
            mockDice.Setup(d => d.Roll(1).d2()).Returns(2);

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void RaceGeneratorReturnsMaleForDrowWizard()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClass>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockDice.Verify(d => d.Roll(It.IsAny<Int32>()).d2(), Times.Never);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void RaceGeneratorReturnsFemaleForDrowCleric()
        {
            characterClass.ClassName = CharacterClassConstants.Cleric;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClass>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockDice.Verify(d => d.Roll(It.IsAny<Int32>()).d2(), Times.Never);
            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void LargeIfInLargeGroup()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), characterClass)).Returns("base race");
            mockCollectionsSelector.Setup(s => s.SelectFrom("BaseRaceGroups", RaceConstants.Sizes.Large)).Returns(new[] { "other base race", "base race" });

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Size, Is.EqualTo(RaceConstants.Sizes.Large));
        }

        [Test]
        public void MediumSizeIsDefault()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), characterClass)).Returns("base race");
            mockCollectionsSelector.Setup(s => s.SelectFrom("BaseRaceGroups", It.IsAny<String>())).Returns(new[] { "other base race" });
            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Size, Is.EqualTo(RaceConstants.Sizes.Medium));
        }

        [Test]
        public void SmallIfInSmallGroup()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), characterClass)).Returns("base race");
            mockCollectionsSelector.Setup(s => s.SelectFrom("BaseRaceGroups", RaceConstants.Sizes.Small)).Returns(new[] { "other base race", "base race" });

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Size, Is.EqualTo(RaceConstants.Sizes.Small));
        }

        [TestCase(RaceConstants.Metaraces.HalfCelestial)]
        [TestCase(RaceConstants.Metaraces.HalfFiend)]
        public void HaveWings(String metarace)
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), characterClass)).Returns(metarace);
            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.True);
        }

        [Test]
        public void NoWings()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), characterClass)).Returns("metarace");
            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.False);
        }

        [Test]
        public void HalfDragonsHaveWingsIfLarge()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), characterClass)).Returns("base race");
            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);
            mockCollectionsSelector.Setup(s => s.SelectFrom("BaseRaceGroups", RaceConstants.Sizes.Large)).Returns(new[] { "base race", "other base race" });

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.True);
        }

        [Test]
        public void HalfDragonsDoNotHaveWingsIfNotLarge()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), characterClass)).Returns("base race");
            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);
            mockCollectionsSelector.Setup(s => s.SelectFrom("BaseRaceGroups", RaceConstants.Sizes.Large)).Returns(new[] { "different base race", "other base race" });

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.False);
        }
    }
}