using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.CharacterClasses;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators
{
    [TestFixture]
    public class CharacterClassGeneratorTests
    {
        private const string ClassName = "class name";

        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<IBooleanPercentileSelector> mockBooleanPercentileSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private ICharacterClassGenerator characterClassGenerator;
        private Alignment alignment;
        private Dictionary<string, int> specialistFieldQuantities;
        private Dictionary<string, int> prohibitedFieldQuantities;
        private List<string> specialistFields;
        private List<string> prohibitedFields;

        [SetUp]
        public void Setup()
        {
            mockBooleanPercentileSelector = new Mock<IBooleanPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            characterClassGenerator = new CharacterClassGenerator(mockAdjustmentsSelector.Object, mockCollectionsSelector.Object, mockBooleanPercentileSelector.Object);

            alignment = new Alignment();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            specialistFieldQuantities = new Dictionary<string, int>();
            prohibitedFieldQuantities = new Dictionary<string, int>();
            specialistFields = new List<string>();
            prohibitedFields = new List<string>();

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SpecialistFieldQuantities)).Returns(specialistFieldQuantities);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ProhibitedFieldQuantities)).Returns(prohibitedFieldQuantities);
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(ClassName);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, ClassName)).Returns(specialistFields);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, ClassName)).Returns(prohibitedFields);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.First());
        }

        [Test]
        public void GeneratorReturnsRandomizedLevel()
        {
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(9266);

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.Level, Is.EqualTo(9266));
        }

        [Test]
        public void GeneratorReturnsRandomizedClass()
        {
            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.ClassName, Is.EqualTo(ClassName));
        }

        [Test]
        public void DoNotGetSpecialistFieldsIfShouldNotHaveAny()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(false);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 1");

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.SpecialistFields, Is.Empty);
        }

        [Test]
        public void DoNotGetSpecialistFieldsIfNone()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.SpecialistFields, Is.Empty);
        }

        [Test]
        public void GetSpecialistField()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 1");
            specialistFields.Add("field 2");

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 2")))).Returns("field 2");

            prohibitedFieldQuantities["field 1"] = 0;
            prohibitedFieldQuantities["field 2"] = 0;

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.SpecialistFields, Contains.Item("field 2"));
            Assert.That(characterClass.SpecialistFields.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetSpecialistFields()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 2;
            specialistFields.Add("field 1");
            specialistFields.Add("field 2");
            specialistFields.Add("field 3");

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 3") && fs.Contains("field 1"))))
                .Returns("field 3").Returns("field 1");

            prohibitedFieldQuantities["field 1"] = 0;
            prohibitedFieldQuantities["field 2"] = 0;
            prohibitedFieldQuantities["field 3"] = 0;

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.SpecialistFields, Contains.Item("field 3"));
            Assert.That(characterClass.SpecialistFields, Contains.Item("field 1"));
            Assert.That(characterClass.SpecialistFields.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CannotSpecializeInAlignmentFieldThatDoesNotMatchAlignment()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("alignment field");
            specialistFields.Add("non-alignment field");

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString()))
                .Returns(new[] { "non-alignment field" });

            prohibitedFieldQuantities["alignment field"] = 0;

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("alignment field")))).Returns("alignment field");

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.SpecialistFields, Contains.Item("alignment field"));
            Assert.That(characterClass.SpecialistFields.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGetProhibitedFieldsIfThereAreNoSpecialistFields()
        {
            prohibitedFields.Add("field 1");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 1")))).Returns("field 1");

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.SpecialistFields, Is.Empty);
            Assert.That(characterClass.ProhibitedFields, Is.Empty);
        }

        [Test]
        public void DoNotGetProhibitedFieldsIfNone()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 1");

            prohibitedFieldQuantities["field 1"] = 1;

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.ProhibitedFields, Is.Empty);
        }

        [Test]
        public void GetProhibitedField()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 1");

            prohibitedFieldQuantities["field 1"] = 1;
            prohibitedFields.Add("field 1");
            prohibitedFields.Add("field 2");

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 2"));
            Assert.That(characterClass.ProhibitedFields.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ProhibitedFieldCannotAlreadyBeASpecialistField()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 1");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 1")))).Returns("field 1");

            prohibitedFieldQuantities["field 1"] = 1;
            prohibitedFields.Add("field 1");
            prohibitedFields.Add("field 2");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 2")))).Returns("field 2");

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 2"));
            Assert.That(characterClass.ProhibitedFields.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetProhibitedFields()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 2");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 2")))).Returns("field 2");

            prohibitedFieldQuantities["field 2"] = 2;
            prohibitedFields.Add("field 1");
            prohibitedFields.Add("field 2");
            prohibitedFields.Add("field 3");
            prohibitedFields.Add("field 4");
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 1") && fs.Contains("field 4"))))
                .Returns("field 4").Returns("field 1");

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 4"));
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 1"));
            Assert.That(characterClass.ProhibitedFields.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetProhibitedFieldsFromMultipleSpecialistFields()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 2;
            specialistFields.Add("field 1");
            specialistFields.Add("field 2");
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 1") && fs.Contains("field 2"))))
                .Returns("field 1").Returns("field 2");

            prohibitedFieldQuantities["field 1"] = 1;
            prohibitedFieldQuantities["field 2"] = 1;
            prohibitedFields.Add("field 1");
            prohibitedFields.Add("field 2");
            prohibitedFields.Add("field 3");
            prohibitedFields.Add("field 4");
            prohibitedFields.Add("field 5");
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 3") && fs.Contains("field 5"))))
                .Returns("field 5").Returns("field 3");

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 5"));
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 3"));
            Assert.That(characterClass.ProhibitedFields.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNotASpecialist_ReturnNothingForRegeneratedSpecialistField()
        {
            var characterClass = new CharacterClass();
            characterClass.ClassName = ClassName;
            var race = new Race();
            race.Metarace = "metarace";

            specialistFields.Add("specialist field");
            specialistFields.Add("other specialist field");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace))
                .Returns(new[] { "metarace specialist field", "other specialist field" });

            var regeneratedSpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(regeneratedSpecialistFields, Is.Empty);
        }

        [Test]
        public void IfASpecialistButNoMetarace_ReturnOriginalSpecialistFields()
        {
            var characterClass = new CharacterClass();
            characterClass.ClassName = ClassName;
            characterClass.SpecialistFields = new[] { "specialist field" };
            var race = new Race();

            specialistFields.Add("specialist field");
            specialistFields.Add("other specialist field");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace))
                .Returns(new[] { "metarace specialist field", "other specialist field" });

            var regeneratedSpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(regeneratedSpecialistFields, Is.EqualTo(characterClass.SpecialistFields));
        }

        [Test]
        public void IfASpecialistButMetaraceDoesNotAffect_ReturnOriginalSpecialistFields()
        {
            var characterClass = new CharacterClass();
            characterClass.ClassName = ClassName;
            characterClass.SpecialistFields = new[] { "specialist field" };
            var race = new Race();
            race.Metarace = "metarace";

            specialistFields.Add("specialist field");
            specialistFields.Add("other specialist field");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace))
                .Returns(new[] { "metarace specialist field" });

            var regeneratedSpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(regeneratedSpecialistFields, Is.EqualTo(characterClass.SpecialistFields));
        }

        [Test]
        public void IfASpecialistAndMetaraceAffects_ReturnNewSpecialistFields()
        {
            var characterClass = new CharacterClass();
            characterClass.ClassName = ClassName;
            characterClass.SpecialistFields = new[] { "specialist field" };
            var race = new Race();
            race.Metarace = "metarace";

            specialistFields.Add("specialist field");
            specialistFields.Add("other specialist field");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace))
                .Returns(new[] { "metarace specialist field", "other specialist field" });

            var regeneratedSpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(regeneratedSpecialistFields.Single(), Is.EqualTo("other specialist field"));
        }

        [Test]
        public void OnlyUseAlignmentFieldsWhenRegeneratingSpecialistFields()
        {
            var characterClass = new CharacterClass();
            characterClass.ClassName = ClassName;
            characterClass.SpecialistFields = new[] { "specialist field" };
            var race = new Race();
            race.Metarace = "metarace";

            specialistFields.Add("specialist field");
            specialistFields.Add("other specialist field");

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString()))
                .Returns(new[] { "non-alignment field" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace))
                .Returns(new[] { "metarace specialist field", "non-alignment field", "other specialist field" });

            var regeneratedSpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);

            Assert.That(regeneratedSpecialistFields.Single(), Is.EqualTo("other specialist field"));
        }

        [Test]
        public void RegenerateMultipleSpecialistFields()
        {
            var characterClass = new CharacterClass();
            characterClass.ClassName = ClassName;
            characterClass.SpecialistFields = new[] { "original specialist field", "other original specialist field" };
            var race = new Race();
            race.Metarace = "metarace";

            specialistFields.Add("specialist field");
            specialistFields.Add("other specialist field");
            specialistFields.Add("metarace specialist field");
            specialistFields.Add("original specialist field");
            specialistFields.Add("other original specialist field");

            var metaraceFields = new[] { "metarace specialist field", "other specialist field", "specialist field" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace))
                .Returns(metaraceFields);

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Intersect(metaraceFields).Any())))
                .Returns("metarace specialist field").Returns("specialist field");

            var regeneratedSpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(regeneratedSpecialistFields, Contains.Item("specialist field"));
            Assert.That(regeneratedSpecialistFields, Contains.Item("metarace specialist field"));
            Assert.That(regeneratedSpecialistFields.Count(), Is.EqualTo(2));
        }
    }
}