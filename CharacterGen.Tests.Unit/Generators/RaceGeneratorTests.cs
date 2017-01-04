using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators
{
    [TestFixture]
    public class RaceGeneratorTests
    {
        private const string BaseRace = "base race";
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
        private Dictionary<string, int> ages;

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

            characterClass.Name = "class name";
            characterClass.Level = 36;
            alignment.Goodness = "goodness";
            speeds[BaseRace] = 9266;

            SetUpTablesForBaseRace(BaseRace);

            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsSum()).Returns(5);

            var mockPartial = new Mock<PartialRoll>();
            mockPartial.Setup(r => r.d(It.IsAny<int>())).Returns(endRoll.Object);
            mockDice.Setup(d => d.Roll(characterClass.Level)).Returns(mockPartial.Object);

            SetUpRoll("42d600", 14);
            SetUpRoll("420d6000", 24);
            SetUpRoll("4200d60000", 34);
            SetUpRoll("10d4", 104);
            SetUpRoll("92d66", 424);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds)).Returns(speeds);
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(BaseRace);
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(Metarace);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, CharacterClassConstants.TrainingTypes.Intuitive))
                .Returns(intuitiveClasses);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, CharacterClassConstants.TrainingTypes.Trained))
                .Returns(trainedClasses);
        }

        private void SetUpRoll(string roll, int result)
        {
            var mockPartial = new Mock<PartialRoll>();
            mockPartial.Setup(r => r.AsSum()).Returns(result);
            mockDice.Setup(d => d.Roll(roll)).Returns(mockPartial.Object);
        }

        private void SetUpRoll(int quantity, int die, int result)
        {
            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsSum()).Returns(result);

            var mockPartial = new Mock<PartialRoll>();
            mockPartial.Setup(r => r.d(die)).Returns(endRoll.Object);
            mockDice.Setup(d => d.Roll(quantity)).Returns(mockPartial.Object);
        }

        private void SetUpTablesForBaseRace(string baseRace)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, CharacterClassConstants.TrainingTypes.Trained);
            mockCollectionsSelector.Setup(s => s.SelectFrom(tableName, baseRace)).Returns(new[] { "4200d60000" });

            tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, CharacterClassConstants.TrainingTypes.SelfTaught);
            mockCollectionsSelector.Setup(s => s.SelectFrom(tableName, baseRace)).Returns(new[] { "420d6000" });

            tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, CharacterClassConstants.TrainingTypes.Intuitive);
            mockCollectionsSelector.Setup(s => s.SelectFrom(tableName, baseRace)).Returns(new[] { "42d600" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MaximumAgeRolls, baseRace)).Returns(new[] { "4d26" });
            SetUpRoll("4d26", 426);

            ages = new Dictionary<string, int>();
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
            characterClass.Name = CharacterClassConstants.Wizard;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.BaseRaces.Drow);
            speeds[RaceConstants.BaseRaces.Drow] = 9266;

            SetUpTablesForBaseRace(RaceConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.IsMale, Is.True);
        }

        [Test]
        public void RaceGeneratorReturnsFemaleForDrowCleric()
        {
            characterClass.Name = CharacterClassConstants.Cleric;
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
            intuitiveClasses.Add(characterClass.Name);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90229));
        }

        [Test]
        public void GetSelfTaughtAge()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90239));
        }

        [Test]
        public void GetTrainedAge()
        {
            trainedClasses.Add(characterClass.Name);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90249));
        }

        [Test]
        public void GetAdultAge()
        {
            intuitiveClasses.Add(characterClass.Name);

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
            mockDice.Setup(d => d.Roll("420d6000").AsSum()).Returns(34);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Stage, Is.EqualTo(RaceConstants.Ages.Old));
        }

        [Test]
        public void GetVenerableAge()
        {
            mockDice.Setup(d => d.Roll("420d6000").AsSum()).Returns(44);

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
            Assert.That(race.WeightInPounds, Is.EqualTo(44118));
        }

        [Test]
        public void GetFemaleWeight()
        {
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.Male)).Returns(false);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.WeightInPounds, Is.EqualTo(44098));
        }

        [Test]
        public void GetMaximumAge()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Maximum, Is.EqualTo(90676));
        }

        [Test]
        public void IntuitiveIsOneThirdOfMax()
        {
            ages[RaceConstants.Ages.Venerable] = ages[RaceConstants.Ages.Adulthood] + 34;
            mockDice.Setup(d => d.Roll("4d26").AsSum()).Returns(100);

            intuitiveClasses.Add(characterClass.Name);
            mockDice.Setup(d => d.Roll(characterClass.Level).d(2).AsSum()).Returns(13);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90237));
        }

        [Test]
        public void SelfTaughtIsTwoThirdsOfMax()
        {
            ages[RaceConstants.Ages.Venerable] = ages[RaceConstants.Ages.Adulthood] + 44;
            mockDice.Setup(d => d.Roll("4d26").AsSum()).Returns(100);

            mockDice.Setup(d => d.Roll(characterClass.Level).d(4).AsSum()).Returns(13);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90247));
        }

        [Test]
        public void TrainedIsMax()
        {
            ages[RaceConstants.Ages.Venerable] = ages[RaceConstants.Ages.Adulthood] + 54;
            mockDice.Setup(d => d.Roll("4d26").AsSum()).Returns(100);

            trainedClasses.Add(characterClass.Name);
            mockDice.Setup(d => d.Roll(characterClass.Level).d(6).AsSum()).Returns(13);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90257));
        }

        [Test]
        public void IfAdditionalAgeDieLessThan1_UseLevel()
        {
            ages[RaceConstants.Ages.Venerable] = ages[RaceConstants.Ages.Adulthood] + 34;
            mockDice.Setup(d => d.Roll("4d26").AsSum()).Returns(39);

            intuitiveClasses.Add(characterClass.Name);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90224 + characterClass.Level));
        }

        [Test]
        public void IfAgeGreaterThanMax_UseMax()
        {
            mockDice.Setup(d => d.Roll(characterClass.Level).d(It.IsAny<int>()).AsSum()).Returns(12345);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90676));
            Assert.That(race.Age.Years, Is.EqualTo(race.Age.Maximum));
        }
    }
}