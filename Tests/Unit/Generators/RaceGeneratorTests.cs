﻿using CharacterGen.Common.Alignments;
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
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators
{
    [TestFixture]
    public class RaceGeneratorTests
    {
        private const string BaseRace = "baseRace";
        private const string Metarace = "metarace";

        private Mock<IBooleanPercentileSelector> mockBooleanPercentileSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<Dice> mockDice;
        private IRaceGenerator raceGenerator;
        private Mock<RaceRandomizer> mockBaseRaceRandomizer;
        private Mock<RaceRandomizer> mockMetaraceRandomizer;
        private CharacterClass characterClass;
        private Alignment alignment;
        private Dictionary<string, int> speeds;
        private List<string> intuitiveClasses;
        private List<string> trainedClasses;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockBooleanPercentileSelector = new Mock<IBooleanPercentileSelector>();
            mockDice = new Mock<Dice>();
            raceGenerator = new RaceGenerator(mockBooleanPercentileSelector.Object, mockCollectionsSelector.Object, mockAdjustmentsSelector.Object, mockDice.Object);

            mockBaseRaceRandomizer = new Mock<RaceRandomizer>();
            mockMetaraceRandomizer = new Mock<RaceRandomizer>();
            characterClass = new CharacterClass();
            alignment = new Alignment();
            speeds = new Dictionary<string, int>();
            intuitiveClasses = new List<string>();
            trainedClasses = new List<string>();

            characterClass.ClassName = "class name";
            alignment.Goodness = "goodness";
            speeds[BaseRace] = 9266;

            SetUpTablesForBaseRace(BaseRace);

            mockDice.Setup(d => d.Roll("42d600")).Returns(14);
            mockDice.Setup(d => d.Roll("420d6000")).Returns(24);
            mockDice.Setup(d => d.Roll("4200d60000")).Returns(34);
            mockDice.Setup(d => d.Roll("10d4")).Returns(104);
            mockDice.Setup(d => d.Roll("92d66")).Returns(426);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds)).Returns(speeds);
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(BaseRace);
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(Metarace);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Intuitive))
                .Returns(intuitiveClasses);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Trained))
                .Returns(trainedClasses);
        }

        private void SetUpTablesForBaseRace(string baseRace)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, GroupConstants.Trained);
            mockCollectionsSelector.Setup(s => s.SelectFrom(tableName, baseRace)).Returns(new[] { "4200d60000" });

            tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, GroupConstants.SelfTaught);
            mockCollectionsSelector.Setup(s => s.SelectFrom(tableName, baseRace)).Returns(new[] { "420d6000" });

            tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, GroupConstants.Intuitive);
            mockCollectionsSelector.Setup(s => s.SelectFrom(tableName, baseRace)).Returns(new[] { "42d600" });

            var ages = new Dictionary<string, int>();
            ages[RaceConstants.Ages.Adulthood] = 90210;
            ages[RaceConstants.Ages.MiddleAge] = 90230;
            ages[RaceConstants.Ages.Old] = 90240;
            ages[RaceConstants.Ages.Venerable] = 90250;

            var maleHeights = new Dictionary<string, int>();
            var femaleHeights = new Dictionary<string, int>();
            var maleWeights = new Dictionary<string, int>();
            var femaleWeights = new Dictionary<string, int>();

            maleHeights[baseRace] = 209;
            femaleHeights[baseRace] = 902;
            maleWeights[baseRace] = 22;
            femaleWeights[baseRace] = 2;

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, baseRace);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(ages);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.HeightRolls, baseRace))
                .Returns(new[] { "10d4" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.WeightRolls, baseRace))
                .Returns(new[] { "92d66" });

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, "Male");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(maleHeights);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, "Female");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(femaleHeights);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, "Male");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(maleWeights);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, "Female");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(femaleWeights);
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

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.IsMale, Is.True);
        }

        [Test]
        public void ReturnFemale()
        {
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.Male)).Returns(false);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.IsMale, Is.False);
        }

        [Test]
        public void RaceGeneratorReturnsMaleForDrowWizard()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.BaseRaces.Drow);
            speeds[RaceConstants.BaseRaces.Drow] = 9266;

            SetUpTablesForBaseRace(RaceConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.IsMale, Is.True);
        }

        [Test]
        public void RaceGeneratorReturnsFemaleForDrowCleric()
        {
            characterClass.ClassName = CharacterClassConstants.Cleric;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.BaseRaces.Drow);
            speeds[RaceConstants.BaseRaces.Drow] = 9266;

            SetUpTablesForBaseRace(RaceConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.IsMale, Is.False);
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
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, It.IsAny<string>())).Returns(new[] { "other base race" });
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
        public void HaveWings(string metarace)
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
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.DragonSpecies, It.IsAny<string>()), Times.Never);
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

        [Test]
        public void GetIntuitiveAge()
        {
            intuitiveClasses.Add(characterClass.ClassName);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90224));
        }

        [Test]
        public void GetSelfTaughtAge()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90234));
        }

        [Test]
        public void GetTrainedAge()
        {
            trainedClasses.Add(characterClass.ClassName);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90244));
        }

        [Test]
        public void GetAdultAge()
        {
            intuitiveClasses.Add(characterClass.ClassName);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Stage, Is.EqualTo(RaceConstants.Ages.Adulthood));
        }

        [Test]
        public void GetMiddleAge()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Stage, Is.EqualTo(RaceConstants.Ages.MiddleAge));
        }

        [Test]
        public void GetOldAge()
        {
            trainedClasses.Add(characterClass.ClassName);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Stage, Is.EqualTo(RaceConstants.Ages.Old));
        }

        [Test]
        public void GetVenerableAge()
        {
            mockDice.Setup(d => d.Roll("4200d60000")).Returns(44);
            trainedClasses.Add(characterClass.ClassName);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Stage, Is.EqualTo(RaceConstants.Ages.Venerable));
        }

        [Test]
        public void GetMaleHeight()
        {
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.Male)).Returns(true);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HeightInInches, Is.EqualTo(313));
        }

        [Test]
        public void GetFemaleHeight()
        {
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.Male)).Returns(false);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HeightInInches, Is.EqualTo(1006));
        }

        [Test]
        public void GetMaleWeight()
        {
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.Male)).Returns(true);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.WeightInPounds, Is.EqualTo(44326));
        }

        [Test]
        public void GetFemaleWeight()
        {
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.Male)).Returns(false);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.WeightInPounds, Is.EqualTo(44306));
        }
    }
}