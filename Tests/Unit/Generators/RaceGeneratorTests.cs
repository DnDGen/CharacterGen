using System;
using System.Collections.Generic;
using D20Dice;
using Moq;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
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
        private Alignment alignment;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Dictionary<String, Int32> speeds;
        private List<String> dragonSpecies;
        private NameModel baseRace;
        private NameModel metarace;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            raceGenerator = new RaceGenerator(mockDice.Object, mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);

            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            characterClass = new CharacterClass();
            alignment = new Alignment();
            speeds = new Dictionary<String, Int32>();
            dragonSpecies = new List<String>();

            baseRace = new NameModel();
            baseRace.Id = "baseRace";

            metarace = new NameModel();
            metarace.Id = "metarace";

            mockDice.Setup(d => d.Roll(1).d2()).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);
            alignment.Goodness = "goodness";
            speeds[baseRace.Id] = 9266;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds)).Returns(speeds);
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment.Goodness, characterClass)).Returns(baseRace);
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment.Goodness, characterClass)).Returns(metarace);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.DragonSpecies, alignment.ToString())).Returns(dragonSpecies);
            dragonSpecies.Add("dragon species");
        }

        [Test]
        public void RandomizeBaseRace()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.BaseRace, Is.EqualTo(baseRace));
        }

        [Test]
        public void RandomizeMetarace()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Metarace, Is.EqualTo(metarace));
        }

        [Test]
        public void ReturnMaleOnLowRoll()
        {
            mockDice.Setup(d => d.Roll(1).d2()).Returns(1);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void ReturnFemaleOnHighRoll()
        {
            mockDice.Setup(d => d.Roll(1).d2()).Returns(2);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void RaceGeneratorReturnsMaleForDrowWizard()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            baseRace.Id = RaceConstants.BaseRaces.DrowId;
            speeds[RaceConstants.BaseRaces.Drow] = 9266;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockDice.Verify(d => d.Roll(It.IsAny<Int32>()).d2(), Times.Never);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void RaceGeneratorReturnsFemaleForDrowCleric()
        {
            characterClass.ClassName = CharacterClassConstants.Cleric;
            baseRace.Id = RaceConstants.BaseRaces.DrowId;
            speeds[RaceConstants.BaseRaces.Drow] = 9266;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockDice.Verify(d => d.Roll(It.IsAny<Int32>()).d2(), Times.Never);
            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void LargeIfInLargeGroup()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large)).Returns(new[] { "other base race", baseRace.Id });

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Size, Is.EqualTo(RaceConstants.Sizes.Large));
        }

        [Test]
        public void MediumSizeIsDefault()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, It.IsAny<String>())).Returns(new[] { "other base race" });
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Size, Is.EqualTo(RaceConstants.Sizes.Medium));
        }

        [Test]
        public void SmallIfInSmallGroup()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Small)).Returns(new[] { "other base race", baseRace.Id });

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Size, Is.EqualTo(RaceConstants.Sizes.Small));
        }

        [TestCase(RaceConstants.Metaraces.HalfCelestialId)]
        [TestCase(RaceConstants.Metaraces.HalfFiendId)]
        public void HaveWings(String metaraceId)
        {
            metarace.Id = metaraceId;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.True);
        }

        [Test]
        public void NoWings()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.False);
        }

        [Test]
        public void HalfDragonsHaveWingsIfLarge()
        {
            metarace.Id = RaceConstants.Metaraces.HalfDragonId;
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large)).Returns(new[] { baseRace.Id, "other base race" });

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large), Times.Once);
            Assert.That(race.HasWings, Is.True);
        }

        [Test]
        public void HalfDragonsDoNotHaveWingsIfNotLarge()
        {
            metarace.Id = RaceConstants.Metaraces.HalfDragonId;
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large)).Returns(new[] { "different base race", "other base race" });

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large), Times.Once);
            Assert.That(race.HasWings, Is.False);
        }

        [Test]
        public void DetermineSpeciesOfHalfDragonByAlignment()
        {
            metarace.Id = RaceConstants.Metaraces.HalfDragonId;
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.MetaraceSpecies, Is.EqualTo("dragon species"));
        }

        [Test]
        public void DetermineSpeciesOfHalfDragonByRandomWithinAlignment()
        {
            metarace.Id = RaceConstants.Metaraces.HalfDragonId;
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            dragonSpecies.Add("dragon species 2");
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.MetaraceSpecies, Is.EqualTo("dragon species 2"));
        }

        [Test]
        public void NonHalfDragonsDoNotDetermineSpecies()
        {
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.DragonSpecies, It.IsAny<String>()), Times.Never);
            Assert.That(race.MetaraceSpecies, Is.Empty);
        }

        [Test]
        public void DetermineLandSpeed()
        {
            speeds["other base race"] = 42;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.LandSpeed, Is.EqualTo(9266));
        }

        [Test]
        public void IfNoWings_AerialSpeedIsZero()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.False);
            Assert.That(race.AerialSpeed, Is.EqualTo(0));
        }

        [Test]
        public void HalfCelestialsHaveAerialSpeedOfTwiceLandSpeed()
        {
            metarace.Id = RaceConstants.Metaraces.HalfCelestialId;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.True);
            Assert.That(race.AerialSpeed, Is.EqualTo(9266 * 2));
        }

        [Test]
        public void HalfFiendsHaveAerialSpeedEqualToLandSpeed()
        {
            metarace.Id = RaceConstants.Metaraces.HalfFiendId;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.True);
            Assert.That(race.AerialSpeed, Is.EqualTo(9266));
        }

        [Test]
        public void HalfDragonsWithWingsHaveAerialSpeedOfTwiceLandSpeed()
        {
            metarace.Id = RaceConstants.Metaraces.HalfDragonId;
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large)).Returns(new[] { baseRace.Id, "otherbaserace" });

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.True);
            Assert.That(race.AerialSpeed, Is.EqualTo(9266 * 2));
        }

        [Test]
        public void HalfDragonsWithoutWingsHaveNoAerialSpeed()
        {
            metarace.Id = RaceConstants.Metaraces.HalfDragonId;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.False);
            Assert.That(race.AerialSpeed, Is.EqualTo(0));
        }
    }
}