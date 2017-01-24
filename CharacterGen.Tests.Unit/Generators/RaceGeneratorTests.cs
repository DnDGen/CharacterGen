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
using System.Linq;

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
        private Dictionary<string, int> landSpeeds;
        private Dictionary<string, int> aerialSpeeds;
        private Dictionary<string, int> ages;
        private Dictionary<string, int> challengeRatings;
        private Dictionary<string, int> maleHeights;
        private Dictionary<string, int> femaleHeights;
        private Dictionary<string, int> maleWeights;
        private Dictionary<string, int> femaleWeights;
        private string classType;
        private string baseRaceSize;
        private List<string> baseRacesWithWings;
        private List<string> metaracesWithWings;

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
            landSpeeds = new Dictionary<string, int>();
            aerialSpeeds = new Dictionary<string, int>();
            classType = CharacterClassConstants.TrainingTypes.Intuitive;
            baseRaceSize = RaceConstants.Sizes.Medium;
            baseRacesWithWings = new List<string>();
            metaracesWithWings = new List<string>();
            challengeRatings = new Dictionary<string, int>();
            maleHeights = new Dictionary<string, int>();
            femaleHeights = new Dictionary<string, int>();
            maleWeights = new Dictionary<string, int>();
            femaleWeights = new Dictionary<string, int>();

            characterClass.Name = "class name";
            characterClass.Level = 36;
            alignment.Goodness = "goodness";

            SetUpTablesForBaseRace(BaseRace);
            SetUpTablesForMetarace(Metarace);

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

            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(BaseRace);
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(Metarace);
            mockCollectionsSelector.Setup(s => s.FindGroupOf(TableNameConstants.Set.Collection.ClassNameGroups, characterClass.Name, CharacterClassConstants.TrainingTypes.Intuitive, CharacterClassConstants.TrainingTypes.SelfTaught, CharacterClassConstants.TrainingTypes.Trained))
                .Returns(() => classType);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.HasWings)).Returns(baseRacesWithWings);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.HasWings)).Returns(metaracesWithWings);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ChallengeRatings, It.IsAny<string>())).Returns((string table, string name) => challengeRatings[name]);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.AerialSpeeds, It.IsAny<string>())).Returns((string table, string name) => aerialSpeeds[name]);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds, It.IsAny<string>())).Returns((string table, string name) => landSpeeds[name]);

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, "Male");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName, It.IsAny<string>())).Returns((string table, string name) => maleHeights[name]);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, "Female");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName, It.IsAny<string>())).Returns((string table, string name) => femaleHeights[name]);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, "Male");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName, It.IsAny<string>())).Returns((string table, string name) => maleWeights[name]);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, "Female");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName, It.IsAny<string>())).Returns((string table, string name) => femaleWeights[name]);
        }

        private void SetUpRoll(string roll, int result)
        {
            var mockPartial = new Mock<PartialRoll>();
            mockPartial.Setup(r => r.AsSum()).Returns(result);
            mockDice.Setup(d => d.Roll(roll)).Returns(mockPartial.Object);
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

            maleHeights[baseRace] = 209;
            femaleHeights[baseRace] = 902;
            maleWeights[baseRace] = 22;
            femaleWeights[baseRace] = 2;

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, baseRace);
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(ages);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName, It.IsAny<string>())).Returns((string table, string name) => ages[name]);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.HeightRolls, baseRace)).Returns(new[] { "10d4" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.WeightRolls, baseRace)).Returns(new[] { "92d66" });
            mockCollectionsSelector.Setup(s => s.FindGroupOf(TableNameConstants.Set.Collection.BaseRaceGroups, baseRace, It.IsAny<string[]>())).Returns(() => baseRaceSize);

            if (landSpeeds.Any())
                landSpeeds[baseRace] = landSpeeds.Last().Value + 1;
            else
                landSpeeds[baseRace] = 9266;

            aerialSpeeds[baseRace] = 0;
            challengeRatings[baseRace] = 0;
        }

        private void SetUpTablesForMetarace(string metarace)
        {
            aerialSpeeds[metarace] = 0;
            challengeRatings[metarace] = 0;
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
            mockCollectionsSelector.Setup(s => s.FindGroupOf(TableNameConstants.Set.Collection.ClassNameGroups, CharacterClassConstants.Wizard, CharacterClassConstants.TrainingTypes.Intuitive, CharacterClassConstants.TrainingTypes.SelfTaught, CharacterClassConstants.TrainingTypes.Trained))
                .Returns(() => classType);

            SetUpTablesForBaseRace(RaceConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.IsMale, Is.True);
        }

        [Test]
        public void RaceGeneratorReturnsFemaleForDrowCleric()
        {
            characterClass.Name = CharacterClassConstants.Cleric;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.BaseRaces.Drow);
            mockCollectionsSelector.Setup(s => s.FindGroupOf(TableNameConstants.Set.Collection.ClassNameGroups, CharacterClassConstants.Cleric, CharacterClassConstants.TrainingTypes.Intuitive, CharacterClassConstants.TrainingTypes.SelfTaught, CharacterClassConstants.TrainingTypes.Trained))
                .Returns(() => classType);

            SetUpTablesForBaseRace(RaceConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.IsMale, Is.False);
        }

        [Test]
        public void GetRaceSize()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Size, Is.EqualTo(RaceConstants.Sizes.Medium));
        }

        [Test]
        public void MetaraceHasWings()
        {
            metaracesWithWings.Add(Metarace);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);

            Assert.That(race.Metarace, Is.EqualTo(Metarace));
            Assert.That(race.HasWings, Is.True);
        }

        [Test]
        public void BaseRaceHasWings()
        {
            baseRacesWithWings.Add(BaseRace);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.BaseRace, Is.EqualTo(BaseRace));
            Assert.That(race.HasWings, Is.True);
        }

        [Test]
        public void BaseRaceAndMetaraceHaveWings()
        {
            baseRacesWithWings.Add(BaseRace);
            metaracesWithWings.Add(Metarace);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.BaseRace, Is.EqualTo(BaseRace));
            Assert.That(race.Metarace, Is.EqualTo(Metarace));
            Assert.That(race.HasWings, Is.True);
        }

        [TestCase(RaceConstants.Sizes.Colossal)]
        [TestCase(RaceConstants.Sizes.Gargantuan)]
        [TestCase(RaceConstants.Sizes.Huge)]
        [TestCase(RaceConstants.Sizes.Large)]
        [TestCase(RaceConstants.Sizes.Medium)]
        [TestCase(RaceConstants.Sizes.Small)]
        [TestCase(RaceConstants.Sizes.Tiny)]
        public void BaseRaceAndHalfDragonHaveWings(string size)
        {
            SetUpTablesForMetarace(RaceConstants.Metaraces.HalfDragon);
            aerialSpeeds[RaceConstants.Metaraces.HalfDragon] = 2;
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);

            baseRacesWithWings.Add(BaseRace);

            baseRaceSize = size;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.BaseRace, Is.EqualTo(BaseRace));
            Assert.That(race.Metarace, Is.EqualTo(RaceConstants.Metaraces.HalfDragon));
            Assert.That(race.HasWings, Is.True);
        }

        [Test]
        public void NoWings()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.False);
        }

        [TestCase(RaceConstants.Sizes.Colossal)]
        [TestCase(RaceConstants.Sizes.Gargantuan)]
        [TestCase(RaceConstants.Sizes.Huge)]
        [TestCase(RaceConstants.Sizes.Large)]
        public void HalfDragonsHaveWings(string size)
        {
            SetUpTablesForMetarace(RaceConstants.Metaraces.HalfDragon);
            aerialSpeeds[RaceConstants.Metaraces.HalfDragon] = 2;
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);

            baseRaceSize = size;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.True);
        }

        [TestCase(RaceConstants.Sizes.Medium)]
        [TestCase(RaceConstants.Sizes.Small)]
        [TestCase(RaceConstants.Sizes.Tiny)]
        public void HalfDragonsDoNotHaveWings(string size)
        {
            SetUpTablesForMetarace(RaceConstants.Metaraces.HalfDragon);
            aerialSpeeds[RaceConstants.Metaraces.HalfDragon] = 2;
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);

            baseRaceSize = size;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.HasWings, Is.False);
        }

        [Test]
        public void DetermineSpeciesOfHalfDragonByRandomWithinAlignment()
        {
            SetUpTablesForMetarace(RaceConstants.Metaraces.HalfDragon);
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            aerialSpeeds[RaceConstants.Metaraces.HalfDragon] = 2;
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
            landSpeeds["other base race"] = 42;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.LandSpeed, Is.EqualTo(9266));
        }

        [Test]
        public void MetaraceAerialSpeedIsSet()
        {
            aerialSpeeds[Metarace] = 30;
            aerialSpeeds[BaseRace] = 20;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.AerialSpeed, Is.EqualTo(30));
        }

        [Test]
        public void BaseRaceAerialSpeedIsSet()
        {
            SetUpTablesForMetarace(RaceConstants.Metaraces.HalfDragon);

            aerialSpeeds[RaceConstants.Metaraces.HalfDragon] = 2;
            aerialSpeeds[BaseRace] = 20;

            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);
            baseRaceSize = RaceConstants.Sizes.Large;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.AerialSpeed, Is.EqualTo(20));
        }

        [Test]
        public void MetaraceAerialSpeedIsMultiplierWithWings()
        {
            SetUpTablesForMetarace(RaceConstants.Metaraces.HalfDragon);
            aerialSpeeds[RaceConstants.Metaraces.HalfDragon] = 2;
            aerialSpeeds[BaseRace] = 0;

            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);
            baseRaceSize = RaceConstants.Sizes.Large;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.AerialSpeed, Is.EqualTo(9266 * 2));
        }

        [Test]
        public void MetaraceAerialSpeedIsMultiplierWithoutWings()
        {
            SetUpTablesForMetarace(RaceConstants.Metaraces.HalfDragon);
            aerialSpeeds[RaceConstants.Metaraces.HalfDragon] = 2;
            aerialSpeeds[BaseRace] = 0;

            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, characterClass)).Returns(RaceConstants.Metaraces.HalfDragon);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.AerialSpeed, Is.EqualTo(0));
        }

        [Test]
        public void NoAerialSpeed()
        {
            aerialSpeeds[Metarace] = 0;
            aerialSpeeds[BaseRace] = 0;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.AerialSpeed, Is.EqualTo(0));
        }

        [Test]
        public void GetIntuitiveAge()
        {
            classType = CharacterClassConstants.TrainingTypes.Intuitive;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90229));
        }

        [Test]
        public void GetSelfTaughtAge()
        {
            classType = CharacterClassConstants.TrainingTypes.SelfTaught;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90239));
        }

        [Test]
        public void GetTrainedAge()
        {
            classType = CharacterClassConstants.TrainingTypes.Trained;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90249));
        }

        [Test]
        public void GetAdultAge()
        {
            classType = CharacterClassConstants.TrainingTypes.Intuitive;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90229));
            Assert.That(race.Age.Stage, Is.EqualTo(RaceConstants.Ages.Adulthood));
        }

        [Test]
        public void GetMiddleAge()
        {
            classType = CharacterClassConstants.TrainingTypes.SelfTaught;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90239));
            Assert.That(race.Age.Stage, Is.EqualTo(RaceConstants.Ages.MiddleAge));
        }

        [Test]
        public void GetOldAge()
        {
            classType = CharacterClassConstants.TrainingTypes.Trained;
            mockDice.Setup(d => d.Roll("420d6000").AsSum()).Returns(34);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90249));
            Assert.That(race.Age.Stage, Is.EqualTo(RaceConstants.Ages.Old));
        }

        [Test]
        public void GetVenerableAge()
        {
            classType = CharacterClassConstants.TrainingTypes.Trained;
            mockDice.Setup(d => d.Roll("4200d60000").AsSum()).Returns(44);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90259));
            Assert.That(race.Age.Stage, Is.EqualTo(RaceConstants.Ages.Venerable));
        }

        [TestCase(RaceConstants.Ages.Adulthood, RaceConstants.Ages.Ageless, RaceConstants.Ages.Ageless, RaceConstants.Ages.Ageless)]
        [TestCase(RaceConstants.Ages.MiddleAge, 90230, RaceConstants.Ages.Ageless, RaceConstants.Ages.Ageless)]
        [TestCase(RaceConstants.Ages.Old, 90230, 90240, RaceConstants.Ages.Ageless)]
        public void GetAgeless(string ageStage, int middleAge, int old, int venerable)
        {
            classType = CharacterClassConstants.TrainingTypes.Trained;
            mockDice.Setup(d => d.Roll("4200d60000").AsSum()).Returns(44);

            ages[RaceConstants.Ages.MiddleAge] = middleAge;
            ages[RaceConstants.Ages.Old] = old;
            ages[RaceConstants.Ages.Venerable] = venerable;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90259));
            Assert.That(race.Age.Stage, Is.EqualTo(ageStage));
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

            classType = CharacterClassConstants.TrainingTypes.Intuitive;
            mockDice.Setup(d => d.Roll(characterClass.Level).d(2).AsSum()).Returns(13);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90237));
        }

        [Test]
        public void SelfTaughtIsTwoThirdsOfMax()
        {
            ages[RaceConstants.Ages.Venerable] = ages[RaceConstants.Ages.Adulthood] + 44;
            mockDice.Setup(d => d.Roll("4d26").AsSum()).Returns(100);

            classType = CharacterClassConstants.TrainingTypes.SelfTaught;
            mockDice.Setup(d => d.Roll(characterClass.Level).d(4).AsSum()).Returns(13);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90247));
        }

        [Test]
        public void TrainedIsMax()
        {
            ages[RaceConstants.Ages.Venerable] = ages[RaceConstants.Ages.Adulthood] + 54;
            mockDice.Setup(d => d.Roll("4d26").AsSum()).Returns(100);

            classType = CharacterClassConstants.TrainingTypes.Trained;
            mockDice.Setup(d => d.Roll(characterClass.Level).d(6).AsSum()).Returns(13);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90257));
        }

        [Test]
        public void IfAdditionalAgeDieLessThan1_UseLevel()
        {
            ages[RaceConstants.Ages.Venerable] = ages[RaceConstants.Ages.Adulthood] + 34;
            mockDice.Setup(d => d.Roll("4d26").AsSum()).Returns(39);

            classType = CharacterClassConstants.TrainingTypes.Intuitive;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90224 + characterClass.Level));
        }

        [Test]
        public void IfAgeGreaterThanMax_UseMax()
        {
            mockDice.Setup(d => d.Roll(characterClass.Level).d(It.IsAny<int>()).AsSum()).Returns(9999999);

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Age.Years, Is.EqualTo(90676));
            Assert.That(race.Age.Years, Is.EqualTo(race.Age.Maximum));
        }

        [Test]
        public void SetBaseRaceChallengeRating()
        {
            challengeRatings[BaseRace] = 9266;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.ChallengeRating, Is.EqualTo(9266));
        }

        [Test]
        public void SetMetaraceChallengeRating()
        {
            challengeRatings[Metarace] = 90210;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.ChallengeRating, Is.EqualTo(90210));
        }

        [Test]
        public void SetBaseRaceAndMetaraceChallengeRating()
        {
            challengeRatings[BaseRace] = 9266;
            challengeRatings[Metarace] = 90210;

            var race = raceGenerator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.ChallengeRating, Is.EqualTo(9266 + 90210));
        }
    }
}