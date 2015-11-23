using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators;
using CharacterGen.Generators.Domain;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using RollGen;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators
{
    [TestFixture]
    public class RaceGeneratorTests
    {
        private const String BaseRace = "baseRace";
        private const String Metarace = "metarace";

        private Mock<IBooleanPercentileSelector> mockBooleanPercentileSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IDice> mockDice;
        private IRaceGenerator raceGenerator;
        private Mock<RaceRandomizer> mockBaseRaceRandomizer;
        private Mock<RaceRandomizer> mockMetaraceRandomizer;
        private CharacterClass characterClass;
        private Alignment alignment;
        private Dictionary<String, Int32> speeds;
        private Dictionary<String, Int32> ageRolls;
        private Dictionary<String, Int32> heightRolls;
        private Dictionary<String, Int32> weightRolls;
        private List<String> intuitiveClasses;
        private List<String> trainedClasses;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockBooleanPercentileSelector = new Mock<IBooleanPercentileSelector>();
            mockDice = new Mock<IDice>();
            raceGenerator = new RaceGenerator(mockBooleanPercentileSelector.Object, mockCollectionsSelector.Object,
                mockAdjustmentsSelector.Object, mockDice.Object);

            mockBaseRaceRandomizer = new Mock<RaceRandomizer>();
            mockMetaraceRandomizer = new Mock<RaceRandomizer>();
            characterClass = new CharacterClass();
            alignment = new Alignment();
            speeds = new Dictionary<String, Int32>();
            ageRolls = new Dictionary<String, Int32>();
            heightRolls = new Dictionary<String, Int32>();
            weightRolls = new Dictionary<String, Int32>();
            intuitiveClasses = new List<String>();
            trainedClasses = new List<String>();

            characterClass.ClassName = "class name";
            alignment.Goodness = "goodness";
            speeds[BaseRace] = 9266;

            ageRolls[AdjustmentConstants.Quantity + GroupConstants.Intuitive] = 42;
            ageRolls[AdjustmentConstants.Die + GroupConstants.Intuitive] = 600;
            ageRolls[AdjustmentConstants.Quantity + GroupConstants.SelfTaught] = 420;
            ageRolls[AdjustmentConstants.Die + GroupConstants.SelfTaught] = 6000;
            ageRolls[AdjustmentConstants.Quantity + GroupConstants.Trained] = 4200;
            ageRolls[AdjustmentConstants.Die + GroupConstants.Trained] = 60000;
            ageRolls[RaceConstants.Ages.Adulthood] = 90210;
            ageRolls[RaceConstants.Ages.MiddleAge] = 90230;
            ageRolls[RaceConstants.Ages.Old] = 90240;
            ageRolls[RaceConstants.Ages.Venerable] = 90250;

            heightRolls[AdjustmentConstants.Base + Boolean.FalseString] = 902;
            heightRolls[AdjustmentConstants.Quantity] = 10;
            heightRolls[AdjustmentConstants.Die] = 4;

            weightRolls[AdjustmentConstants.Base + Boolean.FalseString] = 2;
            weightRolls[AdjustmentConstants.Quantity] = 92;
            weightRolls[AdjustmentConstants.Die] = 66;

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.RACEAges, BaseRace);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(ageRolls);

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.RACEHeights, BaseRace);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(heightRolls);

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.RACEWeights, BaseRace);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(weightRolls);

            mockDice.Setup(d => d.Roll(42).d(600)).Returns(14);
            mockDice.Setup(d => d.Roll(420).d(6000)).Returns(24);
            mockDice.Setup(d => d.Roll(4200).d(60000)).Returns(34);
            mockDice.Setup(d => d.Roll(10).d(4)).Returns(104);
            mockDice.Setup(d => d.Roll(92).d(66)).Returns(426);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds)).Returns(speeds);
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(BaseRace);
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(Metarace);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Intuitive))
                .Returns(intuitiveClasses);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Trained))
                .Returns(trainedClasses);
        }

        [Test]
        public void RandomizeBaseRace()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.BaseRace, Is.EqualTo(BaseRace));
        }

        [Test]
        public void RandomizeMetarace()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Metarace, Is.EqualTo(Metarace));
        }

        [Test]
        public void ReturnMale()
        {
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.Male)).Returns(true);

            heightRolls[AdjustmentConstants.Base + Boolean.TrueString] = 209;
            weightRolls[AdjustmentConstants.Base + Boolean.TrueString] = 22;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void ReturnFemale()
        {
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.Male)).Returns(false);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void RaceGeneratorReturnsMaleForDrowWizard()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.BaseRaces.Drow);
            speeds[RaceConstants.BaseRaces.Drow] = 9266;

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.RACEAges, RaceConstants.BaseRaces.Drow);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(ageRolls);

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.RACEHeights, RaceConstants.BaseRaces.Drow);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(heightRolls);

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.RACEWeights, RaceConstants.BaseRaces.Drow);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(weightRolls);

            heightRolls[AdjustmentConstants.Base + Boolean.TrueString] = 209;
            weightRolls[AdjustmentConstants.Base + Boolean.TrueString] = 22;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void RaceGeneratorReturnsFemaleForDrowCleric()
        {
            characterClass.ClassName = CharacterClassConstants.Cleric;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.BaseRaces.Drow);
            speeds[RaceConstants.BaseRaces.Drow] = 9266;

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.RACEAges, RaceConstants.BaseRaces.Drow);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(ageRolls);

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.RACEHeights, RaceConstants.BaseRaces.Drow);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(heightRolls);

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.RACEWeights, RaceConstants.BaseRaces.Drow);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(weightRolls);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void LargeIfInLargeGroup()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large)).Returns(new[] { "other base race", BaseRace });
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
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Small)).Returns(new[] { "other base race", BaseRace });
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Size, Is.EqualTo(RaceConstants.Sizes.Small));
        }

        [TestCase(RaceConstants.Metaraces.HalfCelestial)]
        [TestCase(RaceConstants.Metaraces.HalfFiend)]
        public void HaveWings(String metarace)
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(metarace);
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
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large)).Returns(new[] { BaseRace, "other base race" });

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large), Times.Once);
            Assert.That(race.HasWings, Is.True);
        }

        [Test]
        public void HalfDragonsDoNotHaveWingsIfNotLarge()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large)).Returns(new[] { "different base race", "other base race" });

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large), Times.Once);
            Assert.That(race.HasWings, Is.False);
        }

        [Test]
        public void DetermineSpeciesOfHalfDragonByRandomWithinAlignment()
        {
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(TableNameConstants.Set.Collection.DragonSpecies, "lawfulness goodness")).Returns("dragon species");

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.MetaraceSpecies, Is.EqualTo("dragon species"));
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
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfCelestial);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.True);
            Assert.That(race.AerialSpeed, Is.EqualTo(9266 * 2));
        }

        [Test]
        public void HalfFiendsHaveAerialSpeedEqualToLandSpeed()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfFiend);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.True);
            Assert.That(race.AerialSpeed, Is.EqualTo(9266));
        }

        [Test]
        public void HalfDragonsWithWingsHaveAerialSpeedOfTwiceLandSpeed()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large)).Returns(new[] { BaseRace, "otherbaserace" });

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.True);
            Assert.That(race.AerialSpeed, Is.EqualTo(9266 * 2));
        }

        [Test]
        public void HalfDragonsWithoutWingsHaveNoAerialSpeed()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.False);
            Assert.That(race.AerialSpeed, Is.EqualTo(0));
        }

        [Test]
        public void GhostsHaveAerialSpeedButNoWings()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.Ghost);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.False);
            Assert.That(race.AerialSpeed, Is.EqualTo(30));
        }
    }
}